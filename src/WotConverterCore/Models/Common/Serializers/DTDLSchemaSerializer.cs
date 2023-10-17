using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WotConverterCore.Models.DigitalTwin.Schema;

namespace WotConverterCore.Models.Serializers
{
    internal class DTDLSchemaSerializer : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            JObject jObject = JObject.Load(reader);

            var baseObject = new DTDLBaseSchema();
            if (jObject.Type == JTokenType.String)
            {
                baseObject = existingValue as string ?? (string)serializer.ContractResolver.ResolveContract(typeof(string)).DefaultCreator();
                using (var subReader = jObject.CreateReader())
                    serializer.Populate(subReader, baseObject);

                return baseObject;
            }

            var parsedType = jObject["@type"]?.ToString();
            var baseObjectType = Enum.Parse(typeof(DTDLSchemaType), parsedType, true);

            switch (baseObjectType)
            {
                case DTDLSchemaType.Array:
                    baseObject = existingValue as DTDLArraySchema ?? (DTDLArraySchema)serializer.ContractResolver.ResolveContract(typeof(DTDLArraySchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case DTDLSchemaType.Object:
                    baseObject = existingValue as DTDLObjectSchema ?? (DTDLObjectSchema)serializer.ContractResolver.ResolveContract(typeof(DTDLObjectSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case DTDLSchemaType.Enum:
                    baseObject = existingValue as DTDLEnumSchema ?? (DTDLEnumSchema)serializer.ContractResolver.ResolveContract(typeof(DTDLEnumSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case DTDLSchemaType.Map:
                    baseObject = existingValue as DTDLMapSchema ?? (DTDLMapSchema)serializer.ContractResolver.ResolveContract(typeof(DTDLMapSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case null:
                default:
                    baseObject = existingValue as string ?? (string)serializer.ContractResolver.ResolveContract(typeof(string)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
            }

            return baseObject;
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {

            var entry = (DTDLBaseSchema?)untypedValue;

            if (entry == null)
                return;

            switch (entry.Type)
            {
                case DTDLSchemaType.Array:
                case DTDLSchemaType.Object:
                case DTDLSchemaType.Enum:
                case DTDLSchemaType.Map:
                    serializer.Serialize(writer, untypedValue);
                    return;
                default:
                    serializer.Serialize(writer, entry.Type);
                    return;

            }
        }

        public override bool CanConvert(Type t) => t == typeof(DTDLBaseSchema);
    }
}
