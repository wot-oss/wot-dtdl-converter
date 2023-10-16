using Newtonsoft.Json;
using WotConverterCore.Models.Common;

namespace WotConverterCore.Models.Serializers
{
    internal class GenericStringArraySerializer : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new GenericStringArray { String = stringValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<string>>(reader);
                    return new GenericStringArray { StringArray = arrayValue };
                default:
                    return new GenericStringArray { };
            }
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            var value = (GenericStringArray?)untypedValue ?? new GenericStringArray();
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.StringArray != null)
            {
                serializer.Serialize(writer, value.StringArray);
                return;
            }

            return;
        }

        public override bool CanConvert(Type t) => t == typeof(GenericStringArray);
    }
}
