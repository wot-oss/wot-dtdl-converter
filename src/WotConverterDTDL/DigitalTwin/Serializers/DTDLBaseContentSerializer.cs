using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WotConverterDTDL.DigitalTwin.Serializers
{
    internal class DTDLBaseContentSerializer : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            JObject jObject = JObject.Load(reader);

            var baseObject = new DTDLBaseContent();
            var tokenType = jObject.SelectToken("@type");

            if (tokenType?.Type == JTokenType.Array)
            {
                var parsedType = tokenType.ToObject<List<string>>();

                if (parsedType?.Contains("Telemetry") ?? false)
                {
                    baseObject = existingValue as DTDLTelemetry ?? (DTDLTelemetry)serializer.ContractResolver.ResolveContract(typeof(DTDLTelemetry)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    return baseObject;
                }
                if (parsedType?.Contains("Property") ?? false)
                {
                    baseObject = existingValue as DTDLProperty ?? (DTDLProperty)serializer.ContractResolver.ResolveContract(typeof(DTDLProperty)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    return baseObject;
                }
                if (parsedType?.Contains("Command") ?? false)
                {
                    baseObject = existingValue as DTDLCommand ?? (DTDLCommand)serializer.ContractResolver.ResolveContract(typeof(DTDLCommand)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    return baseObject;
                }

                return null;
            }
            else if (tokenType?.Type == JTokenType.String)
            {
                var parsedType = tokenType.ToObject<string>();

                switch (parsedType)
                {
                    case "Telemetry":
                        baseObject = existingValue as DTDLTelemetry ?? (DTDLTelemetry)serializer.ContractResolver.ResolveContract(typeof(DTDLTelemetry)).DefaultCreator();
                        using (var subReader = jObject.CreateReader())
                            serializer.Populate(subReader, baseObject);
                        return baseObject;
                    case "Property":
                        baseObject = existingValue as DTDLProperty ?? (DTDLProperty)serializer.ContractResolver.ResolveContract(typeof(DTDLProperty)).DefaultCreator();
                        using (var subReader = jObject.CreateReader())
                            serializer.Populate(subReader, baseObject);
                        return baseObject;
                    case "Command":
                        baseObject = existingValue as DTDLCommand ?? (DTDLCommand)serializer.ContractResolver.ResolveContract(typeof(DTDLCommand)).DefaultCreator();
                        using (var subReader = jObject.CreateReader())
                            serializer.Populate(subReader, baseObject);
                        return baseObject;
                    case null:
                    default:
                        return null;
                }

            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            serializer.Serialize(writer, untypedValue);
            return;
        }

        public override bool CanConvert(Type t) => t == typeof(DTDLBaseContent);

        internal static DTDLBaseContentSerializer Serializer = new DTDLBaseContentSerializer();


    }
}
