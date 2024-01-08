using Newtonsoft.Json.Linq;
using WotConverterCore.Interfaces;
using WotConverterCore.Models.Common;
using WotConverterCore.Models.ThingModel;
using WotConverterCore.Models.ThingModel.DataSchema;
using WotConverterDTDL.DigitalTwin;
using WotConverterDTDL.DigitalTwin.Schema;
// TODO(pedram): lsp tells me this using is unnecessary, but the project won't compile w/o it
using WotConverterCore.Models.Common.Exceptions;

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
                    Id = dtdl.Id,
                    Titles = dtdl.DisplayName?.Dictionary,
                    Description = dtdl.Description?.String ?? $"Created from {dtdl.DisplayName ?? "a"} DTDL model",
                    Descriptions = dtdl.Description?.Dictionary,
                    Context = "https://www.w3.org/2019/wot/td/v1",
                    LdType = "tm:ThingModel",
                    Comment = dtdl.Comment 
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
                    Comment = castedProperty.Comment
                };

                if (tmProperty.DataSchema != null)
                {
                    tmProperty.DataSchema.Unit = castedProperty.Unit;
                }

                var key = castedProperty.Name ?? castedProperty.DisplayName?.ToString() ?? Guid.NewGuid().ToString();

                if (parameters.InsertHrefs)
                {
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
                }

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
                    Titles = castedCommand.DisplayName?.Dictionary,
                    Comment = castedCommand.Comment
                };

                var key = castedCommand.Name ?? castedCommand.DisplayName?.ToString() ?? Guid.NewGuid().ToString();

                if (parameters.InsertHrefs)
                {
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
                }

                var request = GetTMSchema(castedCommand.Request?.Schema);
                var response = GetTMSchema(castedCommand.Response?.Schema);

                if (castedCommand.Request != null && request != null)
                {
                    request.Description = castedCommand.Request.Description?.String;
                    request.Descriptions = castedCommand.Request.Description?.Dictionary;
                    request.Title = castedCommand.Request.DisplayName?.String;
                    request.Titles = castedCommand.Request.DisplayName?.Dictionary;
                    request.Comment = castedCommand.Request.Comment;
                    request.Unit = castedCommand.Request.Unit;

                    tmAction.Input = request;
                }

                if (castedCommand.Response != null && response != null)
                {
                    response.Description = castedCommand.Response.Description?.String;
                    response.Descriptions = castedCommand.Response.Description?.Dictionary;
                    response.Title = castedCommand.Response.DisplayName?.String;
                    response.Titles = castedCommand.Response.DisplayName?.Dictionary;
                    response.Comment = castedCommand.Response.Comment;
                    response.Unit = castedCommand.Response.Unit;                    

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
                    Comment = castedTelemetry.Comment
                };

                tmEvent.DataResponse.Unit = castedTelemetry.Unit;

                var key = castedTelemetry.Name ?? castedTelemetry.DisplayName?.ToString() ?? Guid.NewGuid().ToString();

                if (parameters.InsertHrefs)
                {
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
                }
                tm.AddEvent(key, tmEvent);
            }
        }

        private static BaseDataSchema? GetTMSchema(DTDLBaseSchema? schema)
        {
            if (schema == null)
                return null;

            switch (schema.Type.Enumerator)
            {
                case DTDLSchemaType.Boolean:
                    return new BooleanSchema();

                case DTDLSchemaType.Date:
                    var dateResult = new StringSchema();
                    dateResult.Format = "date";
                    return dateResult;

                case DTDLSchemaType.DateTime:
                    var dateTimeResult = new StringSchema();
                    dateTimeResult.Format = "date-time";
                    return dateTimeResult;

                case DTDLSchemaType.Double:
                    return new NumberSchema()
                    {
                        LdType = "xsd:double"
                    };
                        
                case DTDLSchemaType.Float:
                    return new NumberSchema()
                    {
                        LdType = "xsd:float"
                    };
                        

                case DTDLSchemaType.Duration:
                    var durationResult = new StringSchema();
                    durationResult.Format = "duration";
                    return durationResult;
                    
                case DTDLSchemaType.Long:
                    return new IntegerSchema()
                    {
                        LdType = "xsd:long"
                    };

                case DTDLSchemaType.Integer:
                    return new IntegerSchema()
                    {
                        LdType = "xsd:int"
                    };
                    
                case DTDLSchemaType.String:
                    var stringResult = new StringSchema();
                    return stringResult;
                    
                case DTDLSchemaType.Time:
                    var timeResult = new StringSchema();
                    timeResult.Format = "time";
                    return timeResult;

                case DTDLSchemaType.Array:
                    var castedArraySchema = (DTDLArraySchema)schema;
                    if (castedArraySchema == null)
                    {
                        return null;
                    }

                    var arrayResult = new ArraySchema()
                    {
                        Description = castedArraySchema?.Description?.String,
                        Descriptions = castedArraySchema?.Description?.Dictionary,
                        Title = castedArraySchema?.DisplayName?.String,
                        Titles = castedArraySchema?.DisplayName?.Dictionary,
                        Comment = castedArraySchema?.Comment
                    };
                        
                    arrayResult.Items = GetTMSchema(castedArraySchema?.ElementSchema);

                    return arrayResult;
                    
                case DTDLSchemaType.Enum:
                    var castedEnumSchema = (DTDLEnumSchema)schema;
                    if (castedEnumSchema == null)
                        return null;

                    var listOfEnums = castedEnumSchema?
                        .GetEnumValues()?
                        .Select(d => new ConstObject {
                                Const = d.EnumValue,
                                Title = d.DisplayName?.String,
                                Titles = d.DisplayName?.Dictionary,
                                Descriptions = d.Description?.Dictionary,
                                Description = d.Description?.String,
                                Comment = d.Comment
                            })?.ToList() ?? new();

                    Console.WriteLine(listOfEnums.First().Name);
                    Console.WriteLine(listOfEnums.First().Const);
                    if (!listOfEnums.Any())
                    {
                        Console.WriteLine("Bailing out");
                        return null;
                    }

                    BaseDataSchema enumResult;
                    switch (castedEnumSchema?.ValueSchema)
                    {
                        case "string":
                            enumResult = new StringSchema()
                            {
                                OneOf = listOfEnums
                            };
                            break;
                        case "integer":
                            enumResult = new IntegerSchema()
                            {
                                OneOf = listOfEnums
                            };                                
                            break;
                        default:
                            throw new InvalidDTDLSchema();
                    }
                    
                    
                    return enumResult;

                case DTDLSchemaType.Map:
                    Console.WriteLine("Map schema not yet implemented. Skipping schema.");
                    return null;
                    
                case DTDLSchemaType.Object:

                    var castedObjectSchema = (DTDLObjectSchema)schema;
                    if (castedObjectSchema == null)
                        return null;

                    var objectResult = new ObjectSchema()
                    {
                        Title = castedObjectSchema.DisplayName?.String,
                        Titles = castedObjectSchema.DisplayName?.Dictionary,
                        Descriptions = castedObjectSchema.Description?.Dictionary,
                        Description = castedObjectSchema.Description?.String,
                        Comment = castedObjectSchema.Comment
                    };

                    foreach (var item in castedObjectSchema.GetObjectFields() ?? new())
                    {
                        var key = item.Name ?? item.DisplayName?.ToString();
                        var value = GetTMSchema(item.Schema);
                        if (value != null)
                        {
                            value.Comment = item.Comment;
                            value.Unit = item.Unit;
                            objectResult.AddObjectProperty(new KeyValuePair<string, BaseDataSchema>(
                                key: key,
                                value: value
                            ));
                        }
                    }

                    return objectResult;

                case DTDLSchemaType.LineString:
                case DTDLSchemaType.MultiLineString:
                case DTDLSchemaType.MultiPoint:
                case DTDLSchemaType.MultiPolygon:
                case DTDLSchemaType.Point:
                case DTDLSchemaType.Polygon:
                    Console.WriteLine("GeoSpatial Schema not yet implemented. Using string.");
                    return new StringSchema();
                    
                default:
                    throw new InvalidDTDLSchema();
            }
        }
    }
}
