using Newtonsoft.Json;
using WotConverterCore.Models.Common;

namespace WotConverterCore.Models.Serializers
{
    public class GenericStringArraySerializer<T> : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new GenericStringArray<T> { String = stringValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<T>>(reader);
                    return new GenericStringArray<T> { Array = arrayValue };
                default:
                    return new GenericStringArray<T> { };
            }
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            var value = (GenericStringArray<T>?)untypedValue ?? new GenericStringArray<T>();
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.Array != null)
            {
                serializer.Serialize(writer, value.Array);
                return;
            }

            return;
        }

        public override bool CanConvert(Type t) => t == typeof(GenericStringArray<T>);
    }
}
