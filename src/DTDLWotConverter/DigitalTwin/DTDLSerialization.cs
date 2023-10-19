using DTDLWotConverter.DigitalTwin.Schema;
using Newtonsoft.Json;
using WotConverterCore.Models.Common;

namespace DTDLWotConverter.DigitalTwin
{
    public partial class DTDL
    {
        private static JsonSerializerSettings SerializationSettings = new JsonSerializerSettings
        {
            Converters =
            {
                GenericStringEnum<DTDLSchemaType>.Serializer,
                GenericStringInt.Serializer,
                DTDLBaseSchema.Serializer,
                DTDLBaseContent.Serializer
            },
            NullValueHandling = NullValueHandling.Ignore
        };

        public string Serialize() =>
            JsonConvert.SerializeObject(this, Formatting.Indented, SerializationSettings);

        public static DTDL? Deserialize(string inputString) =>
            JsonConvert.DeserializeObject<DTDL>(inputString, SerializationSettings);

        public static DTDL? Deserialize(StreamReader stream) =>
            JsonConvert.DeserializeObject<DTDL>(stream.ReadToEnd(), SerializationSettings);
    }
}
