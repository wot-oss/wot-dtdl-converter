using Newtonsoft.Json;
using WotConverterCore.Models.Common;

namespace WotConverterCore.Models.Serializers
{
    public class GenericStringBoolSerializer : JsonConverter
    {

        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                    var stringBool = serializer.Deserialize<string>(reader);
                    return new GenericStringBool { StringBool = stringBool };
                case JsonToken.Boolean:
                    var boolean = serializer.Deserialize<bool>(reader);
                    return new GenericStringBool { Bool = boolean };
                default:
                    return new GenericStringBool { };
            }
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            var value = (GenericStringBool?)untypedValue ?? new GenericStringBool();
            if (value.Bool != null)
            {
                serializer.Serialize(writer, value.Bool);
                return;
            }
            if (value.StringBool != null)
            {
                serializer.Serialize(writer, value.StringBool);
                return;
            }

            return;
        }
        public override bool CanConvert(Type t) => t == typeof(GenericStringBool);

        public static readonly GenericStringBoolSerializer Serializer = new GenericStringBoolSerializer();

    }
}
