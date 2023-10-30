using Newtonsoft.Json.Linq;
using WotConverterCore.Interfaces;
using WotConverterCore.Models.Common;
using WotConverterCore.Models.ThingModel;
using WotConverterCore.Models.ThingModel.DataSchema;
using WotConverterDTDL.DigitalTwin;
using WotConverterDTDL.DigitalTwin.Schema;

namespace WotConverterDTDL.Converters
{
    public class DTDL2TM
    {
        public static TM? DTDL2ThingModel(DTDL dtdl, TMDefaultConversionParameters? p = null)
        {
            try
            {
                TM tm = new()
                {
                    Title = dtdl.DisplayName?.String,
                    Titles = dtdl.DisplayName?.Dictionary,
                    Description = dtdl.Description?.String ?? $"Creted from {dtdl.DisplayName ?? "a"} DTDL model",
                    Descriptions = dtdl.Description?.Dictionary,
                    Context = "https://www.w3.org/2019/wot/td/v1",
                    LdType = "tm:ThingModel"
                };

                //DTDL Properties
                CreateTMProperties(ref tm, dtdl, p);

                //DTDL Commands
                CreateTMActions(ref tm, dtdl, p);

                //DTDL Telemetry
                CreateTMEvents(ref tm, dtdl, p);

                return tm;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        private static void CreateTMProperties(ref TM tm, DTDL dtdl, IConversionParameters? parameters = null)
        {
            var dtldlProperties = dtdl
                .GetDTDLContents()
                .Where(_ => _ != null && typeof(DTDLProperty) == _.GetType())?
                .ToList() ?? new();

            //TODO: Enum, Object, Map values
            foreach (var property in dtldlProperties)
            {
                var castedProperty = (DTDLProperty)property;

                Property tmProperty = new()
                {
                    Title = castedProperty.DisplayName?.String,
                    Titles = castedProperty.DisplayName?.Dictionary,
                    Description = castedProperty.Description?.String,
                    Descriptions = castedProperty.Description?.Dictionary,
                    DataSchema = GetTMSchema(castedProperty.Schema),
                };

                var key = castedProperty.Name ?? castedProperty.DisplayName?.ToString() ?? Guid.NewGuid().ToString();

                tmProperty.Forms = new();
                Form tmForm = new()
                {
                    Href = parameters?.InsertHrefs ?? false ? $"{{{{{key?.ToUpper() ?? Guid.NewGuid().ToString()}_HREF}}}}" : null,
                    Op = new GenericStringArray<OpEnum>()
                };

                tmForm.Op.Array = new()
                {
                        OpEnum.Readproperty
                };

                if (castedProperty.Writable ?? false)
                    tmForm.Op.Array.Add(OpEnum.WriteProperty);

                tmProperty.Forms.Add(tmForm);

                tm.AddProperty(key, tmProperty);
            }
        }

        private static void CreateTMActions(ref TM tm, DTDL dtdl, IConversionParameters? parameters = null)
        {
            var dtdlCommands = dtdl
                .GetDTDLContents()
                .Where(_ => _ != null && typeof(DTDLCommand) == _.GetType())?
                .ToList() ?? new();

            foreach (var command in dtdlCommands)
            {
                var castedCommand = (DTDLCommand)command;

                WotConverterCore.Models.ThingModel.Action tmAction = new()
                {
                    Description = castedCommand.Description?.String,
                    Descriptions = castedCommand.Description?.Dictionary,
                    Title = castedCommand.DisplayName?.String,
                    Titles = castedCommand.DisplayName?.Dictionary
                };

                var key = castedCommand.Name ?? castedCommand.DisplayName?.ToString() ?? Guid.NewGuid().ToString();

                tmAction.Forms = new();
                Form tmForm = new()
                {
                    Href = parameters?.InsertHrefs ?? false ? $"{{{{{key?.ToUpper() ?? Guid.NewGuid().ToString()}_HREF}}}}" : null,
                    Op = new GenericStringArray<OpEnum>()
                };

                tmForm.Op.Array = new()
                {
                         OpEnum.Invokeaction
                };

                tmAction.Forms.Add(tmForm);


                var request = GetTMSchema(castedCommand.Request?.Schema);
                var response = GetTMSchema(castedCommand.Response?.Schema);

                if (castedCommand.Request != null && request != null)
                {
                    request.Description = castedCommand.Request.Description?.String;
                    request.Descriptions = castedCommand.Request.Description?.Dictionary;
                    request.Title = castedCommand.Request.DisplayName?.String;
                    request.Titles = castedCommand.Request.DisplayName?.Dictionary;

                    tmAction.Input = request;
                }

                if (castedCommand.Response != null && response != null)
                {
                    response.Description = castedCommand.Response.Description?.String;
                    response.Descriptions = castedCommand.Response.Description?.Dictionary;
                    response.Title = castedCommand.Response.DisplayName?.String;
                    response.Titles = castedCommand.Response.DisplayName?.Dictionary;

                    tmAction.Output = response;
                }

                tm.AddAction(key, tmAction);
            }
        }

        private static void CreateTMEvents(ref TM tm, DTDL dtdl, IConversionParameters? parameters = null)
        {
            var dtdlTelemetry = dtdl
                .GetDTDLContents()
                .Where(_ => _ != null && typeof(DTDLTelemetry) == _.GetType())?
                .ToList() ?? new();

            //TODO: Enum, Object, Map values
            foreach (var telemetry in dtdlTelemetry)
            {
                var castedTelemetry = (DTDLTelemetry)telemetry;

                Event tmEvent = new()
                {
                    Title = castedTelemetry.DisplayName?.String,
                    Titles = castedTelemetry.DisplayName?.Dictionary,
                    Description = castedTelemetry.Description?.String,
                    Descriptions = castedTelemetry.Description?.Dictionary,
                    DataResponse = GetTMSchema(castedTelemetry.Schema),
                };

                var key = castedTelemetry.Name ?? castedTelemetry.DisplayName?.ToString() ?? Guid.NewGuid().ToString();


                tmEvent.Forms = new();
                Form tmForm = new()
                {
                    Href = parameters?.InsertHrefs ?? false ? $"{{{{{key?.ToUpper() ?? Guid.NewGuid().ToString()}_HREF}}}}" : null,
                    Op = new GenericStringArray<OpEnum>()
                };

                tmForm.Op.Array = new()
                {
                   OpEnum.Subscribeevent
                };

                tmEvent.Forms.Add(tmForm);
                tm.AddEvent(key, tmEvent);
            }
        }

        private static BaseDataSchema? GetTMSchema(DTDLBaseSchema? schema)
        {
            if (schema == null)
                return null;


            switch (schema.Type.Enumerator)
            {
                case DTDLSchemaType.Enum:

                    var castedEnumSchema = (DTDLEnumSchema)schema;
                    if (castedEnumSchema == null)
                        return null;

                    var listOfEnums = castedEnumSchema?
                        .GetEnumValues()?
                        .Select(_ => _.EnumValue?.ToString())?.ToList() ?? new();

                    if (!listOfEnums.Any())
                        return null;

                    var enumResult = new StringSchema()
                    {
                        Enum = listOfEnums,
                        Description = castedEnumSchema?.Description?.String,
                        Descriptions = castedEnumSchema?.Description?.Dictionary,
                        Title = castedEnumSchema?.DisplayName?.String,
                        Titles = castedEnumSchema?.DisplayName?.Dictionary
                    };

                    return enumResult;

                case DTDLSchemaType.String:

                    var stringResult = new StringSchema
                    {
                        Title = schema.DisplayName?.String,
                        Titles = schema.DisplayName?.Dictionary,
                        Description = schema.Description?.String,
                        Descriptions = schema.Description?.Dictionary,

                    };
                    stringResult.Format = schema.Type.Enumerator switch
                    {
                        DTDLSchemaType.DateTime => "dateTime",
                        DTDLSchemaType.Time => "time",
                        DTDLSchemaType.Duration => "duration",
                        DTDLSchemaType.String => null,
                        _ => null
                    };

                    return stringResult;

                case DTDLSchemaType.Object:


                    var castedObjectSchema = (DTDLObjectSchema)schema;
                    if (castedObjectSchema == null)
                        return null;

                    var objectResult = new ObjectSchema()
                    {
                        Title = castedObjectSchema.DisplayName?.String,
                        Titles = castedObjectSchema.DisplayName?.Dictionary,
                        Descriptions = castedObjectSchema.Description?.Dictionary,
                        Description = castedObjectSchema.Description?.String
                    };

                    foreach (var item in castedObjectSchema.GetObjectFields() ?? new())
                    {
                        var key = item.Name ?? item.DisplayName?.ToString();
                        var value = GetTMSchema(item.Schema);
                        if (value != null)
                        {
                            objectResult.AddObjectProperty(new KeyValuePair<string, BaseDataSchema>(
                                key: key,
                                value: value
                            ));
                        }
                    }

                    return objectResult;

                case DTDLSchemaType.Array:

                    var castedArraySchema = (DTDLArraySchema)schema;

                    var arrayResult = new ArraySchema()
                    {
                        Description = schema.Description?.String,
                        Descriptions = schema.Description?.Dictionary,
                        Title = schema.DisplayName?.String,
                        Titles = schema.DisplayName?.Dictionary,
                    };

                    if (castedArraySchema.ElementSchema != null)
                    {
                        arrayResult.Items = GetTMSchema(castedArraySchema.ElementSchema);
                    }

                    return arrayResult;

                case DTDLSchemaType.Double:
                case DTDLSchemaType.Float:
                    return new StringSchema();
                case DTDLSchemaType.Boolean:
                    return new BooleanSchema();
                case DTDLSchemaType.Integer:
                case DTDLSchemaType.Long:
                    return new IntegerSchema();
                default:
                    return new StringSchema();
            }
        }
    }
}