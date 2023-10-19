using Newtonsoft.Json;
using WotConverterCore.Models.Common;

namespace WotConverterCore.Models.Serializers
{
    public class GenericStringDoubleSerializer : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                    var stringDouble = serializer.Deserialize<string>(reader);
                    return new GenericStringDouble { Stringdouble = stringDouble };
                case JsonToken.Float:
                    var number = serializer.Deserialize<double>(reader);
                    return new GenericStringDouble { Double = number };
                default:
                    return new GenericStringDouble { };
            }
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            var value = (GenericStringDouble?)untypedValue ?? new GenericStringDouble();
            if (value.Double != null)
            {
                serializer.Serialize(writer, value.Double);
                return;
            }
            if (value.Stringdouble != null)
            {
                serializer.Serialize(writer, value.Stringdouble);
                return;
            }

            return;
        }

        public override bool CanConvert(Type t) => t == typeof(GenericStringDouble);
    }
}
