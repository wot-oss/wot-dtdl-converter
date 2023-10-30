using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace WotConverterCore.Models.Common.Serializers
{
    public class GenericStringDictionarySerializer<T> : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {

            if (reader.TokenType == JsonToken.Null)
                return null;
;
            var baseObject = new GenericStringDictionary<T>();

            if (reader.TokenType == JsonToken.String)
            {
                var stringValue = serializer.Deserialize<string>(reader);
                baseObject = stringValue;
                return baseObject;
            }

            JObject jObject = JObject.Load(reader);
            var dictValue = existingValue as Dictionary<string, T> ?? (Dictionary<string,T>)serializer.ContractResolver.ResolveContract(typeof(Dictionary<string,T>)).DefaultCreator();
            using (var subReader = jObject.CreateReader())
                serializer.Populate(subReader, dictValue);

            return new GenericStringDictionary<T> { Dictionary = dictValue };
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            var value = (GenericStringDictionary<T>?)untypedValue ?? new GenericStringDictionary<T>();
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

        public override bool CanConvert(Type t) => t == typeof(GenericStringDictionary<T>);

        public static readonly GenericStringDictionarySerializer<T> Serializer = new GenericStringDictionarySerializer<T>();

    }
}
