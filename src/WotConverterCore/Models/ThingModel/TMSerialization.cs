using Newtonsoft.Json;
using WotConverterCore.Models.Common.Serializers;
using WotConverterCore.Models.Serializers;
using WotConverterCore.Models.ThingModel.Serializers;

namespace WotConverterCore.Models.ThingModel
{
    public partial class TM
    {
        private static JsonSerializerSettings SerializationSettings = new JsonSerializerSettings
        {
            Converters =
            {
                GenericStringArraySerializer<string>.Serializer,
                GenericStringArraySerializer<OpEnum>.Serializer,
                GenericStringDoubleSerializer.Serializer,
                GenericStringIntSerializer.Serializer,
                GenericStringBoolSerializer.Serializer,
                GenericStringUriSerializer.Serializer,
                BaseDataSchemaSerializer.Serializer,
                PropertySerializer.Serializer
            },
            NullValueHandling = NullValueHandling.Ignore
        };

        public string Serialize() =>
            JsonConvert.SerializeObject(this, Formatting.Indented, SerializationSettings);

        public static TM? Deserialize(string inputString) =>
            JsonConvert.DeserializeObject<TM>(inputString, SerializationSettings);

        public static TM? Deserialize(StreamReader stream) =>
            JsonConvert.DeserializeObject<TM>(stream.ReadToEnd(), SerializationSettings);

    }
}
