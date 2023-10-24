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
            var parsedType = jObject["@type"]?.ToString();

            switch (parsedType)
            {
                case "Telemetry":
                    baseObject = existingValue as DTDLTelemetry ?? (DTDLTelemetry)serializer.ContractResolver.ResolveContract(typeof(DTDLTelemetry)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case "Property":
                    baseObject = existingValue as DTDLProperty ?? (DTDLProperty)serializer.ContractResolver.ResolveContract(typeof(DTDLProperty)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case "Command":
                    baseObject = existingValue as DTDLCommand ?? (DTDLCommand)serializer.ContractResolver.ResolveContract(typeof(DTDLCommand)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;

                case null:
                default:
                    return null;
            }

            return baseObject;
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
