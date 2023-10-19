using Newtonsoft.Json;
using WotConverterCore.Models.Common;
using WotConverterCore.Models.ThingModel.DataSchema;

namespace WotConverterCore.Models.ThingModel
{
    public partial class TM
    {
        private static JsonSerializerSettings SerializationSettings = new JsonSerializerSettings
        {
            Converters =
            {
                GenericStringArray<string>.Serializer,
                GenericStringArray<OpEnum>.Serializer,
                GenericStringDouble.Serializer,
                GenericStringInt.Serializer,
                GenericStringBool.Serializer,
                GenericStringUri.Serializer,
                BaseDataSchema.Serializer,
                Property.Serializer
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
