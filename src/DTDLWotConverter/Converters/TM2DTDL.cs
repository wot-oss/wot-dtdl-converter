using DTDLWotConverter.DigitalTwin;
using DTDLWotConverter.DigitalTwin.Schema;
using WotConverterCore.Models.ThingModel;
using WotConverterCore.Models.ThingModel.DataSchema;

namespace WotConverterCore.Converters
{
    internal static class Tm2DTDL
    {
        public static DTDL? ThingModel2DTDL(TM tm)
        {
            try
            {
                DTDL dtdl = new()
                {
                    Context = "dtmi:dtdl:context;3",
                    Id = "dtmi:" + tm.Title.ToLowerInvariant().Replace(' ', ':') + ";1",
                    Type = "Interface",
                    DisplayName = tm.Title,
                    Description = tm.Description ?? $"Creted from {tm.Title} thing model",
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
        public static DTDLBaseSchema? TMSchema2DTDLSchema(BaseDataSchema? schema)
        {
            if (schema == null)
                return null;

            switch (schema.Type)
            {
                case TypeEnum.String:

                    if (schema.Enum?.Any() ?? false)
                    {
                        var enumResult = new DTDLEnumSchema("string")
                        {
                            DisplayName = schema.Title,
                            Description = schema.Description
                        };


                        foreach (var item in schema.Enum)
                        {
                            enumResult.AddEnumValue(new DTDLEnumValue
                            {
                                DisplayName = item,
                                Name = item,
                                EnumValue = item
                            });
                        }

                        return enumResult;
                    }

                    else if (schema.Format == "date-time")
                        return "dateTime";
                    else if (schema.Format == "time")
                        return "duration";
                    else
                        return "string";

                case TypeEnum.Object:

                    var objectResult = new DTDLObjectSchema()
                    {
                        DisplayName = schema.Title?.Replace(" ", ""),
                        Description = schema.Description,
                    };

                    var castedTmObjectSchema = (ObjectSchema)schema;
                    foreach (var item in castedTmObjectSchema?.Properties ?? new())
                    {
                        objectResult.AddObjectField(new DTDLObjectField
                        {
                            Description = item.Value.Description,
                            Name = item.Key,
                            DisplayName = item.Value.Title?.Replace(" ", ""),
                            Schema = TMSchema2DTDLSchema(item.Value)
                        });
                    }

                    return objectResult;

                case TypeEnum.Array:
                    var arrayResult = new DTDLArraySchema("string")
                    {
                        DisplayName = schema.Title,
                        Description = schema.Description
                    };

                    var castedTmArraySchema = (ArraySchema)schema;

                    if (castedTmArraySchema.Items != null)
                    {
                        arrayResult.ElementSchema = TMSchema2DTDLSchema(castedTmArraySchema.Items);
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
        private static void CreateDTDLProperties(ref DTDL dtdl, TM tm)
        {
            var tmProperties = tm.GetProperties() ?? new();

            //TODO: Enum, Object, Map values
            foreach (var property in tmProperties)
            {
                var propertyForms = property.Value.Forms;
                var propertyValue = property.Value;

                DTDLProperty content = new()
                {
                    Name = property.Key,
                    DisplayName = (propertyValue.Title ?? property.Key),
                    Description = propertyValue.Description ?? $"Property obtained from '{tm.Title}' Thing Model",
                    Schema = TMSchema2DTDLSchema(propertyValue.DataSchema),
                    Writable = propertyForms.Select(_ => _.HasOpProperty(OpEnum.WriteProperty)).Any(_ => _)
                };

                content.Comment = ComposeFormComment(propertyForms, tm.Base);

                dtdl.Addcontent(content);
            }
        }
        private static void CreateDTDLCommands(ref DTDL dtdl, TM tm)
        {
            var tmActions = tm.GetActions() ?? new();
            foreach (var action in tmActions)
            {

                var actionForms = action.Value.Forms;
                var actionValue = action.Value;

                DTDLCommand content = new()
                {
                    Name = action.Key,
                    DisplayName = actionValue.Title,
                    Description = actionValue.Description ?? $"Command obtained from '{tm.Title}' Thing Model"
                };

                if (actionValue.Input != null)
                {
                    var request = new DTDLCommandRequest
                    {
                        DisplayName = actionValue.Input.Title ?? action.Key + " Request",
                        Name = action.Key + "Request",
                        Description = actionValue.Input.Description,
                        Schema = TMSchema2DTDLSchema(actionValue.Input)
                    };

                    content.Request = request;
                }

                if (actionValue.Output != null)
                {
                    var response = new DTDLCommandResponse
                    {
                        DisplayName = actionValue.Output.Title ?? action.Key + " Response",
                        Name = action.Key + "Response",
                        Description = actionValue.Output.Description,
                        Schema = TMSchema2DTDLSchema(actionValue.Output)
                    };

                    content.Response = response;
                }

                content.Comment = ComposeFormComment(actionForms, tm.Base);

                dtdl.Addcontent(content);
            }
        }
        private static void CreateDTDLTelemetry(ref DTDL dtdl, TM tm)
        {
            var tmEvents = tm.GetEvents() ?? new();

            foreach (var ev in tmEvents)
            {
                var eventForms = ev.Value.Forms;
                var eventValue = ev.Value;
                DTDLTelemetry content = new()
                {
                    Name = ev.Key,
                    DisplayName = eventValue.Title ?? ev.Key,
                    Description = eventValue.Description ?? $"Telemetry obtained from '{tm.Title}' Thing Model",
                    Schema = TMSchema2DTDLSchema(eventValue.DataResponse),
                };

                content.Comment = ComposeFormComment(eventForms, tm.Base);

                dtdl.Addcontent(content);
            }
        }
        private static string? ComposeFormComment(List<Form> forms, string? baseaddress = null)
        {
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

            if ((baseAddress?.ToLower().StartsWith("modbus://") ?? false) || (form.Href?.ToString().ToLower().StartsWith("modbus://") ?? false))
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
                    comment = $"adress={form.ModbusAddress}, ";
                }

                if (form.ModbusQuantity != null)
                {
                    comment?.Trim(':');
                    comment = $"quantity={form.ModbusQuantity}, ";
                }

                if (form.ModbusUnitId != null)
                {
                    comment?.Trim(':');
                    comment = $"unitId={form.ModbusUnitId}";
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
