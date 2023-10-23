using Newtonsoft.Json;

namespace WotConverterCore.Models.Common.Serializers
{
    public class GenericStringUriSerializer : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {

            if (reader.TokenType != JsonToken.String)
                return null;

            var jsonProperty = serializer.Deserialize<string>(reader);
            var isCreated = Uri.TryCreate(jsonProperty, UriKind.RelativeOrAbsolute, out var uri);

            if (isCreated)
                return new GenericStringUri { Uri = uri };

            return new GenericStringUri { StringUri = jsonProperty };

        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            var value = (GenericStringUri?)untypedValue ?? new GenericStringUri();
            if (value.Uri != null)
            {
                serializer.Serialize(writer, value.Uri);
                return;
            }
            if (value.StringUri != null)
            {
                serializer.Serialize(writer, value.StringUri);
                return;
            }

            return;
        }
        public override bool CanConvert(Type t) => t == typeof(GenericStringUri);

        public static readonly GenericStringUriSerializer Serializer = new GenericStringUriSerializer();

    }
}
