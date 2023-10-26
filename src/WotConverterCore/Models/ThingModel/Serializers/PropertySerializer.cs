using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WotConverterCore.Models.Common;
using WotConverterCore.Models.Common.Serializers;
using WotConverterCore.Models.Serializers;
using WotConverterCore.Models.ThingModel.DataSchema;

namespace WotConverterCore.Models.ThingModel.Serializers
{
    internal class PropertySerializer : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            JObject jObject = JObject.Load(reader);

            var baseObject = new BaseDataSchema();
            var parsedType = jObject["type"]?.ToString();
            var property = existingValue as Property ?? (Property)serializer.ContractResolver.ResolveContract(typeof(Property)).DefaultCreator();

            if (parsedType == null)
            {
                baseObject = existingValue as BaseDataSchema ?? (BaseDataSchema)serializer.ContractResolver.ResolveContract(typeof(BaseDataSchema)).DefaultCreator();
                property.DataSchema = baseObject;

                using (var subReader = jObject.CreateReader())
                    serializer.Populate(subReader, property);

                return property;
            }

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

            property.DataSchema = baseObject;

            using (var subReader = jObject.CreateReader())
                serializer.Populate(subReader, property);

            return property;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var property = (Property)untypedValue;
            var propertyJson = JsonConvert.SerializeObject(untypedValue, Formatting.None, new JsonSerializerSettings
            {
                Converters = { 
                    GenericStringUriSerializer.Serializer,
                    GenericStringArraySerializer<OpEnum>.Serializer,
                    GenericStringArraySerializer<string>.Serializer,
                    BaseDataSchemaSerializer.Serializer
                }
            });

            JToken jsonObjectProperty = JObject.Parse(propertyJson);
            jsonObjectProperty = RemoveEmptyChildren(jsonObjectProperty);

            if (property.DataSchema == null)
                serializer.Serialize(writer, jsonObjectProperty);

            var dataSchema = JToken.FromObject(property.DataSchema);
            dataSchema = RemoveEmptyChildren(dataSchema);

            var jsonObjectPropertyObject = (JObject)jsonObjectProperty;
            var dataObject = (JObject)dataSchema;

            jsonObjectPropertyObject.Merge(dataObject, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union,
                MergeNullValueHandling = MergeNullValueHandling.Ignore,
            });

            jsonObjectPropertyObject.Remove("DataSchema");
            serializer.Serialize(writer, jsonObjectPropertyObject);

            return;
        }

        public override bool CanConvert(Type t) => t == typeof(Property);

        internal static PropertySerializer Serializer = new PropertySerializer();
        private static JToken RemoveEmptyChildren(JToken token)
        {
            if (token.Type == JTokenType.Object)
            {
                JObject copy = new JObject();
                foreach (JProperty prop in token.Children<JProperty>())
                {
                    JToken child = prop.Value;
                    if (child.HasValues)
                    {
                        child = RemoveEmptyChildren(child);
                    }
                    if (!IsEmpty(child))
                    {
                        copy.Add(prop.Name, child);
                    }
                }
                return copy;
            }
            else if (token.Type == JTokenType.Array)
            {
                JArray copy = new JArray();
                foreach (JToken item in token.Children())
                {
                    JToken child = item;
                    if (child.HasValues)
                    {
                        child = RemoveEmptyChildren(child);
                    }
                    if (!IsEmpty(child))
                    {
                        copy.Add(child);
                    }
                }
                return copy;
            }
            return token;
        }

        private static bool IsEmpty(JToken token)
        {
            return (token.Type == JTokenType.Null) ||
               (token.Type == JTokenType.Array && !token.HasValues) ||
               (token.Type == JTokenType.Object && !token.HasValues);
        }
    }
}
