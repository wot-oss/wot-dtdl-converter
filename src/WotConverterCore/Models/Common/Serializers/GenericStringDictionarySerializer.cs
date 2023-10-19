using Newtonsoft.Json;

namespace WotConverterCore.Models.Common.Serializers
{
    public class GenericStringDictionarySerializer : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            return new GenericStringDictionary { Dictionary = new Dictionary<string, string>() };
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            var value = (GenericStringDictionary?)untypedValue ?? new GenericStringDictionary();
            if (value.Dictionary != null)
            {
                serializer.Serialize(writer, value.Dictionary);
                return;
            }
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }

            return;
        }

        public override bool CanConvert(Type t) => t == typeof(GenericStringDouble);
    }
}
