using Newtonsoft.Json;
using System.Runtime.Serialization;
using WotConverterCore.Extensions;
using WotConverterCore.Models.Common;
using WotConverterDTDL.DigitalTwin.Serializers;

namespace WotConverterDTDL.DigitalTwin.Schema
{
    public class DTDLBaseSchema
    {
        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("@type")]
        public GenericStringEnum<DTDLSchemaType> Type { get; protected set; }

        [JsonProperty("displayName")]
        public GenericStringDictionary? DisplayName { get; set; }

        [JsonProperty("description")]
        public GenericStringDictionary? Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }

        public DTDLSchemaType? GetSchemaType()
        {
            if (Type.Enumerator != null)
                return Type.Enumerator;

            return null;
        }

        //String Oerators
        public static implicit operator DTDLBaseSchema(DTDLSchemaType type) => new DTDLBaseSchema { Type = type };
        public static implicit operator DTDLBaseSchema(string type) => new DTDLBaseSchema { Type = Enum.Parse<DTDLSchemaType>(type, true) };

        //Should Serialize (Avoid empty objects during serialization)
        public bool ShouldSerializeDescription() => !Description.IsEmpty();
        public bool ShouldSerializeDisplayName() => !DisplayName.IsEmpty();
        public bool ShouldSerializeType() => !Type.IsEmpty();

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
