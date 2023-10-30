using WotConverterCore.Models.Common;
using WotConverterCore.Models.ThingModel;
using WotConverterCore.Models.ThingModel.DataSchema;
using WotConverterDTDL.DigitalTwin;
using WotConverterDTDL.DigitalTwin.Schema;

namespace WotConverterDTDL.Converters
{
    internal static class TM2DTDL
    {
        public static DTDL? ThingModel2DTDL(TM tm)
        {
            try
            {
                DTDL dtdl = new()
                {
                    Context = "dtmi:dtdl:context;3",
                    Id = $"dtmi:{tm.Title?.Replace('-',' ').Replace(@"\s+", " ").Replace(' ', ':')};1" ?? $"dtmi:{Guid.NewGuid()};1",
                    Type = "Interface",
                    Description = GetDTDLLocalizedString(tm.Description, tm.Descriptions?.Dictionary, $"Creted from {tm.Title} thing model"),
                    DisplayName = GetDTDLLocalizedString(tm.Title, tm.Titles?.Dictionary),
                    Comment = tm.Base
                };

                //DTDL Properties
                CreateDTDLProperties(ref dtdl, tm);

                //DTDL Commands
                CreateDTDLCommands(ref dtdl, tm);

                //DTDL Telemetry
                CreateDTDLTelemetry(ref dtdl, tm);

                return dtdl;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        private static void CreateDTDLProperties(ref DTDL dtdl, TM tm)
        {
            var tmProperties = tm.GetProperties() ?? new();

            //TODO: Enum, Object, Map values
            foreach (var property in tmProperties)
            {
                var propertyForms = property.Value.Forms ?? new();
                var propertyValue = property.Value;

                DTDLProperty content = new()
                {
                    Name = property.Key,
                    DisplayName = GetDTDLLocalizedString(propertyValue.Title, propertyValue.Titles?.Dictionary, property.Key),
                    Description = GetDTDLLocalizedString(propertyValue.Description, propertyValue.Descriptions?.Dictionary, $"Property obtained from '{tm.Title}' Thing Model"),
                    Schema = GetDTDLSchema(propertyValue.DataSchema),
                    Writable = propertyForms?.Array?.Select(_ => _.HasOpProperty(OpEnum.WriteProperty)).Any(_ => _) ?? false
                };

                content.Comment = GetFormsComment(propertyForms?.Array, tm.Base);

                dtdl.Addcontent(content);
            }
        }
        private static void CreateDTDLCommands(ref DTDL dtdl, TM tm)
        {
            var tmActions = tm.GetActions() ?? new();
            foreach (var action in tmActions)
            {

                var actionForms = action.Value.Forms ?? new();
                var actionValue = action.Value;

                DTDLCommand content = new()
                {
                    Name = action.Key,
                    DisplayName = GetDTDLLocalizedString(actionValue.Title, actionValue.Titles?.Dictionary),
                    Description = GetDTDLLocalizedString(actionValue.Description, actionValue.Descriptions?.Dictionary, $"Command obtained from '{tm.Title}' Thing Model")
                };

                if (actionValue.Input != null)
                {
                    var request = new DTDLCommandRequest
                    {
                        DisplayName = GetDTDLLocalizedString(actionValue.Input.Title, actionValue.Input.Titles?.Dictionary, $"{action.Key} Request"),
                        Name = action.Key + "Request",
                        Description = GetDTDLLocalizedString(actionValue.Input.Description, actionValue.Input.Descriptions?.Dictionary),
                        Schema = GetDTDLSchema(actionValue.Input)
                    };

                    content.Request = request;
                }

                if (actionValue.Output != null)
                {
                    var response = new DTDLCommandResponse
                    {
                        DisplayName = GetDTDLLocalizedString(actionValue.Output.Title, actionValue.Output.Titles?.Dictionary, $"{action.Key} Response"),
                        Name = action.Key + "Response",
                        Description = GetDTDLLocalizedString(actionValue.Output.Description, actionValue.Output.Descriptions?.Dictionary),
                        Schema = GetDTDLSchema(actionValue.Output)
                    };

                    content.Response = response;
                }

                content.Comment = GetFormsComment(actionForms?.Array, tm.Base);

                dtdl.Addcontent(content);
            }
        }
        private static void CreateDTDLTelemetry(ref DTDL dtdl, TM tm)
        {
            var tmEvents = tm.GetEvents() ?? new();

            foreach (var ev in tmEvents)
            {
                var eventForms = ev.Value.Forms ?? new();
                var eventValue = ev.Value;
                DTDLTelemetry content = new()
                {
                    Name = ev.Key,
                    DisplayName = GetDTDLLocalizedString(eventValue.Title, eventValue.Titles?.Dictionary, ev.Key),
                    Description = GetDTDLLocalizedString(eventValue.Description, eventValue.Descriptions?.Dictionary, $"Telemetry obtained from '{tm.Title}' Thing Model"),
                    Schema = GetDTDLSchema(eventValue.DataResponse),
                };

                content.Comment = GetFormsComment(eventForms?.Array, tm.Base);

                dtdl.Addcontent(content);
            }
        }

        private static DTDLBaseSchema? GetDTDLSchema(BaseDataSchema? schema)
        {
            if (schema == null)
                return null;

            switch (schema.Type)
            {
                case TypeEnum.String:

                    if (schema.Enum?.Array?.Any() ?? false)
                    {
                        var enumResult = new DTDLEnumSchema("string")
                        {
                            DisplayName = GetDTDLLocalizedString(schema.Title, schema.Titles?.Dictionary),
                            Description = GetDTDLLocalizedString(schema.Description, schema.Descriptions?.Dictionary)
                        };


                        foreach (var item in schema.Enum?.Array ?? new())
                        {
                            enumResult.AddEnumValue(new DTDLEnumValue
                            {
                                DisplayName = item,
                                Name = item,
                                EnumValue = item
                            });
                        }

                        if ((!enumResult.GetEnumValues()?.Any() ?? false) && (schema.Enum?.String != null))
                            enumResult.AddEnumValue(new DTDLEnumValue { 
                                DisplayName = schema.Enum.String,
                                Name = schema.Enum.String, 
                                EnumValue = schema.Enum.String 
                            
                            });

                        return enumResult;
                    }

                    else if (schema.Format == "date-time")
                        return "dateTime";
                    else if (schema.Format == "time" || schema.Format == "duration" || schema.Format == "time")
                        return schema.Format;
                    else
                        return "string";

                case TypeEnum.Object:

                    var objectResult = new DTDLObjectSchema()
                    {
                        DisplayName = GetDTDLLocalizedString(
                            schema.Title?.Replace(" ", ""),
                            schema.Titles?.Dictionary?.ToDictionary(_ => _.Key, _ => _.Value.Replace(" ", ""))),
                        Description = GetDTDLLocalizedString(schema.Description, schema.Descriptions?.Dictionary)
                    };

                    var castedTmObjectSchema = (ObjectSchema)schema;
                    foreach (var item in castedTmObjectSchema?.GetObjectProperties() ?? new())
                    {
                        objectResult.AddObjectField(new DTDLObjectField
                        {
                            Description = GetDTDLLocalizedString(item.Value?.Description, item.Value?.Descriptions?.Dictionary),
                            Name = item.Key,
                            DisplayName = GetDTDLLocalizedString(
                               item.Value?.Title?.Replace(" ", ""),
                               item.Value?.Titles?.Dictionary?.ToDictionary(_ => _.Key, _ => _.Value.Replace(" ", ""))),
                            Schema = GetDTDLSchema(item.Value)
                        });
                    }

                    return objectResult;

                case TypeEnum.Array:
                    var arrayResult = new DTDLArraySchema("string")
                    {
                        DisplayName = GetDTDLLocalizedString(schema.Title, schema.Titles?.Dictionary),
                        Description = GetDTDLLocalizedString(schema.Description, schema.Descriptions?.Dictionary)
                    };

                    var castedTmArraySchema = (ArraySchema)schema;

                    if (castedTmArraySchema.Items != null)
                    {
                        arrayResult.ElementSchema = GetDTDLSchema(castedTmArraySchema.Items);
                    }

                    return arrayResult;

                case TypeEnum.Number:
                    return "double";
                case TypeEnum.Boolean:
                    return "boolean";
                case TypeEnum.Integer:
                    return "integer";
                default:
                    return "string";
            }
        }
        private static GenericStringDictionary<string>? GetDTDLLocalizedString(string? value = null, Dictionary<string, string>? values = null, string? defaultValue = null)
        {
            if (values?.Any() ?? false)
                return values;
            else if (value != null)
                return value;
            else
                return defaultValue;
        }
        private static string? GetFormsComment(List<Form>? forms, string? baseaddress = null)
        {
            if (forms == null)
                return null;

            string result = "";

            var formsCount = forms.Count();
            var multipleforms = formsCount > 1;

            if (formsCount > 0)
                result += $"{formsCount} form{(multipleforms ? "s" : "")}: ";

            foreach (Form form in forms)
            {
                var formIndexName = multipleforms ? (forms.IndexOf(form) + 1).ToString() : "1";
                result += $"{formIndexName} - {GetProtocolComment(form, baseaddress)}{(multipleforms ? " / " : "")}";
            }

            return string.IsNullOrWhiteSpace(result) ? null : result;
        }
        private static string? GetProtocolComment(Form form, string? baseAddress = null)
        {
            var comment = string.Empty;

            if ((baseAddress?.ToLower().StartsWith("modbus") ?? false) || (form.Href?.ToString().ToLower().StartsWith("modbus") ?? false))
            {
                comment = $"modbus: href={form.Href}, ";

                if (form.ModbusFunction.HasValue)
                {
                    comment?.Trim(':');
                    comment += $"function={Enum.GetName(form.ModbusFunction.Value)}, ";
                }

                if (form.ModbusAddress != null)
                {
                    comment?.Trim(':');
                    comment += $"adress={form.ModbusAddress}, ";
                }

                if (form.ModbusQuantity != null)
                {
                    comment?.Trim(':');
                    comment += $"quantity={form.ModbusQuantity}, ";
                }

                if (form.ModbusUnitId != null)
                {
                    comment?.Trim(':');
                    comment += $"unitId={form.ModbusUnitId}";
                }

                comment?.TrimEnd(',');
            }
            else
            {
                comment = form.Href == null ? "" : $"href: {form.Href}";
            }

            return string.IsNullOrWhiteSpace(comment) ? null : comment;
        }
    }

}
