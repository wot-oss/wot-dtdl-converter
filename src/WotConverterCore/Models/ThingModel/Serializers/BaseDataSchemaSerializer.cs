using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WotConverterCore.Models.ThingModel.DataSchema;

namespace WotConverterCore.Models.ThingModel.Serializers
{
    internal class BaseDataSchemaSerializer : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            JObject jObject = JObject.Load(reader);

            var baseObject = new BaseDataSchema();
            var parsedType = jObject["type"]?.ToString();
            if (parsedType == null)
                return null;

            var baseObjectType = Enum.Parse(typeof(TypeEnum), parsedType, true);

            switch (baseObjectType)
            {
                case TypeEnum.Array:
                    baseObject = existingValue as ArraySchema ?? (ArraySchema)serializer.ContractResolver.ResolveContract(typeof(ArraySchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.Boolean:
                    baseObject = existingValue as BooleanSchema ?? (BooleanSchema)serializer.ContractResolver.ResolveContract(typeof(BooleanSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.Number:
                    baseObject = existingValue as NumberSchema ?? (NumberSchema)serializer.ContractResolver.ResolveContract(typeof(NumberSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.Integer:
                    baseObject = existingValue as IntegerSchema ?? (IntegerSchema)serializer.ContractResolver.ResolveContract(typeof(IntegerSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.Object:
                    baseObject = existingValue as ObjectSchema ?? (ObjectSchema)serializer.ContractResolver.ResolveContract(typeof(ObjectSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.String:
                    baseObject = existingValue as StringSchema ?? (StringSchema)serializer.ContractResolver.ResolveContract(typeof(StringSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.LineString:
                    baseObject = existingValue as LineStringSchema ?? (LineStringSchema)serializer.ContractResolver.ResolveContract(typeof(LineStringSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;                                        
                case TypeEnum.MultiLineString:
                    baseObject = existingValue as MultiLineStringSchema ?? (MultiLineStringSchema)serializer.ContractResolver.ResolveContract(typeof(MultiLineStringSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.MultiPoint:
                    baseObject = existingValue as MultiPointSchema ?? (MultiPointSchema)serializer.ContractResolver.ResolveContract(typeof(MultiPointSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.MultiPolygon:
                    baseObject = existingValue as MultiPolygonSchema ?? (MultiPolygonSchema)serializer.ContractResolver.ResolveContract(typeof(MultiPolygonSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.Point:
                    baseObject = existingValue as PointSchema ?? (PointSchema)serializer.ContractResolver.ResolveContract(typeof(PointSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.Polygon:
                    baseObject = existingValue as PolygonSchema ?? (PolygonSchema)serializer.ContractResolver.ResolveContract(typeof(PolygonSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;                                        
                case TypeEnum.Null:
                    baseObject = existingValue as NullSchema ?? (NullSchema)serializer.ContractResolver.ResolveContract(typeof(NullSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case null:
                default:
                    baseObject = existingValue as BaseDataSchema ?? (BaseDataSchema)serializer.ContractResolver.ResolveContract(typeof(BaseDataSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
            }

            return baseObject;
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            serializer.Serialize(writer, untypedValue);
            return;
        }

        public override bool CanConvert(Type t) => t == typeof(BaseDataSchema);

        internal static BaseDataSchemaSerializer Serializer = new BaseDataSchemaSerializer();
    }
}
