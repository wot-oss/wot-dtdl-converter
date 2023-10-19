using Newtonsoft.Json;

namespace WotConverterCore.Models.Common.Serializers
{
    public class GenericStringEnumSerializer<T> : JsonConverter where T : struct
    {
        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null || reader.TokenType != JsonToken.String)
                return null;

            var jsonString = serializer.Deserialize<string>(reader) ?? "";
            var isEnum = Enum.TryParse(typeof(T), jsonString, out var enumerator);

            if (isEnum)
            {
                var enumValue = Enum.Parse<T>(jsonString);
                return new GenericStringEnum<T> { Enumerator = enumValue };
            }

            return new GenericStringEnum<T> { StringEnumerator = jsonString };
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            var value = (GenericStringEnum<T>?)untypedValue ?? new GenericStringEnum<T>();
            if (value.StringEnumerator != null)
            {
                serializer.Serialize(writer, value.StringEnumerator);
                return;
            }
            if (!value.Enumerator?.Equals(default) ?? false)
            {
                serializer.Serialize(writer, value.Enumerator);
                return;
            }

            return;
        }

        public override bool CanConvert(Type t) => t == typeof(GenericStringEnum<T>);
    }
}
