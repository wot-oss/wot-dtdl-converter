using Newtonsoft.Json;
using WotConverterCore.Models.Common;

namespace WotConverterCore.Models.Serializers
{
    public class GenericStringIntSerializer : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                    var stringInt = serializer.Deserialize<string>(reader);
                    return new GenericStringInt { StringInt = stringInt };
                case JsonToken.Integer:
                    var integer = serializer.Deserialize<int>(reader);
                    return new GenericStringInt { Integer = integer };
                default:
                    return new GenericStringInt { };
            }
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            var value = (GenericStringInt?)untypedValue ?? new GenericStringInt();
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer);
                return;
            }
            if (value.StringInt != null)
            {
                serializer.Serialize(writer, value.StringInt);
                return;
            }

            return;
        }
        public override bool CanConvert(Type t) => t == typeof(GenericStringInt);

        public static readonly GenericStringIntSerializer Serializer = new GenericStringIntSerializer();

    }
}
