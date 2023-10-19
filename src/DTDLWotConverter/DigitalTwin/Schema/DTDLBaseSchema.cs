using DTDLWotConverter.DigitalTwin.Serializers;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using WotConverterCore.Models.Common;
using WotConverterCore.Models.Common.Interfaces;

namespace DTDLWotConverter.DigitalTwin.Schema
{
    public class DTDLBaseSchema : ISerializable<DTDLSchemaSerializer>
    {
        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("@type")]
        public GenericStringEnum<DTDLSchemaType> Type { get; protected set; }

        internal static DTDLSchemaSerializer Serializer = new DTDLSchemaSerializer();

        public static implicit operator DTDLBaseSchema(DTDLSchemaType type) => new DTDLBaseSchema { Type = type };
        public static implicit operator DTDLBaseSchema(string type) => new DTDLBaseSchema { Type = Enum.Parse<DTDLSchemaType>(type, true) };


        public DTDLSchemaType? GetSchemaType()
        {
            if (Type.Enumerator != null)
                return Type.Enumerator;

            return null;
        }
    }

    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum DTDLSchemaType
    {
        [JsonIgnore]
        Unknown,

        [EnumMember(Value = "string")]
        String,

        [EnumMember(Value = "double")]
        Double,

        [EnumMember(Value = "integer")]
        Integer,

        [EnumMember(Value = "dateTime")]
        DateTime,

        [EnumMember(Value = "boolean")]
        Boolean,

        [EnumMember(Value = "duration")]
        Duration,

        [EnumMember(Value = "Enum")]
        Enum,

        [EnumMember(Value = "Object")]
        Object,

        [EnumMember(Value = "Array")]
        Array,

        [EnumMember(Value = "Map")]
        Map
    };

}
