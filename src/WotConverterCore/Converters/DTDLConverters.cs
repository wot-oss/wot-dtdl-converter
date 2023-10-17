using System.Net;
using WotConverterCore.Models.DigitalTwin;
using WotConverterCore.Models.ThingModel;
using WotConverterCore.Models.ThingModel.DataSchema;

namespace WotConverterCore.Converters
{
    internal static class DTDLConverters
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

        private static void CreateDTDLProperties(ref DTDL dtdl, TM tm)
        {
            var tmProperties = tm.GetProperties() ?? new();

            //TODO: Enum, Object, Map values
            foreach (var property in tmProperties)
            {
                var propertyForms = property.Value.Forms;
                var propertyValue = property.Value;

                foreach (Form form in propertyForms)
                {
                    var formIndexName = (propertyForms.Count() > 1 ? $"_{propertyForms.IndexOf(form)}" : "");
                    var formIndexDisplayName = (propertyForms.Count() > 1 ? $"({propertyForms.IndexOf(form)})" : "");
                    DTDLProperty content = new()
                    {
                        Name = property.Key + formIndexName,
                        DisplayName = propertyValue.Title + formIndexDisplayName,
                        Description = propertyValue.Description ?? $"Property obtained from '{tm.Title}' Thing Model",
                        Schema = GetDTDLSchema(propertyValue.Type, propertyValue.Format),
                        Writable = form.Op?.Contains(Op.WriteProperty) ?? false
                    };

                    content.Comment = GetProtocolComment(form, tm.Base);

                    dtdl.Addcontent(content);
                }
            }
        }
        private static void CreateDTDLCommands(ref DTDL dtdl, TM tm)
        {
            var tmActions = tm.GetActions() ?? new();
            foreach (var action in tmActions)
            {
                var actionForms = action.Value.Forms;
                var actionValue = action.Value;

                foreach (Form form in actionForms)
                {
                    var formIndexName = (actionForms.Count() > 1 ? $"_{actionForms.IndexOf(form)}" : "");
                    var formIndexDisplayName = (actionForms.Count() > 1 ? $"({actionForms.IndexOf(form)})" : "");
                    DTDLCommand content = new()
                    {
                        Name = action.Key + formIndexName,
                        DisplayName = actionValue.Title + formIndexDisplayName,
                        Description = actionValue.Description ?? $"Property obtained from '{tm.Title}' Thing Model"
                    };

                    if (actionValue.Input != null)
                    {
                        var request = new DTDLCommandRequest
                        {
                            DisplayName = actionValue.Input.Title ?? action.Key + " Request",
                            Name = action.Key + "Request" + formIndexName,
                            Description = actionValue.Input.Description,
                            Schema = GetDTDLSchema(actionValue.Input.Type, actionValue.Input.Format)
                        };

                        content.Request = request;
                    }

                    if (actionValue.Output != null)
                    {
                        var response = new DTDLCommandResponse
                        {
                            DisplayName = actionValue.Output.Title ?? action.Key + " Response",
                            Name = action.Key + "Response" + formIndexName,
                            Description = actionValue.Output.Description,
                            Schema = GetDTDLSchema(actionValue.Output.Type, actionValue.Output.Format)
                        };

                        content.Response = response;
                    }

                    content.Comment = GetProtocolComment(form, tm.Base);

                    dtdl.Addcontent(content);
                }
            }
        }
        private static void CreateDTDLTelemetry(ref DTDL dtdl, TM tm)
        {
            var tmEvents = tm.GetEvents() ?? new();

            foreach (var ev in tmEvents)
            {
                var eventForms = ev.Value.Forms;
                var eventValue = ev.Value;

                foreach (Form form in eventForms)
                {
                    var formIndexName = (eventForms.Count() > 1 ? $"_{eventForms.IndexOf(form)}" : "");
                    var formIndexDisplayName = (eventForms.Count() > 1 ? $"({eventForms.IndexOf(form)})" : "");
                    DTDLTelemetry content = new()
                    {
                        Name = ev.Key + formIndexName,
                        DisplayName = eventValue.Title + formIndexDisplayName,
                        Description = eventValue.Description ?? $"Telemetry obtained from '{tm.Title}' Thing Model",
                        Schema = GetDTDLSchema(eventValue.DataResponse?.Type , eventValue.DataResponse?.Format),
                    };

                    content.Comment = GetProtocolComment(form, tm.Base);

                    dtdl.Addcontent(content);
                }
            }
        }

        private static string GetDTDLSchema(TypeEnum? TMType, string? format = null)
        {
            if (TMType == null)
                return "string";

            switch(TMType)
            {
                case TypeEnum.String:
                    if (format == "date-time")
                        return "dateTime";
                    else if (format == "time")
                        return "duration";
                    else
                        return "string";
                case TypeEnum.Number:
                    return "double";
                case TypeEnum.Boolean:
                    return "boolean";
                case TypeEnum.Integer: 
                    return "integer";
                case TypeEnum.Object: 
                    return "Object";
                case TypeEnum.Array: 
                    return "Array";
                default:
                    return "string";
            }
        }

        private static string? GetProtocolComment(Form form, string? baseAddress = null)
        {
            var comment = string.Empty;

            if ((baseAddress?.ToLower().StartsWith("modbus://") ?? false) || (form.Href?.OriginalString.ToLower().StartsWith("modbus://") ?? false))
            {
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
                comment = form.Href == null ? "" : $"Thing Model form href: {form.Href.OriginalString}";
            }

            return string.IsNullOrWhiteSpace(comment) ? null : comment;
        }
    }
}
