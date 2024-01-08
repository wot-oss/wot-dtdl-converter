using Newtonsoft.Json;
using System.Runtime.Serialization;
using WotConverterCore.Extensions;
using WotConverterCore.Models.Common;
using WotConverterDTDL.DigitalTwin.Serializers;

namespace WotConverterDTDL.DigitalTwin.Schema
{
    // TODO(pedram): why is everything initialized to null, e.g. "Unknown" is a sensible default for Type
    public class DTDLBaseSchema
    {
        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("@type")]
        public GenericStringEnum<DTDLSchemaType> Type { get; protected set; }

        [JsonProperty("displayName")]
        public GenericStringDictionary<string>? DisplayName { get; set; }

        [JsonProperty("description")]
        public GenericStringDictionary<string>? Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }

        [JsonProperty("unit")]
        public string? Unit { get; set; }

        public DTDLSchemaType? GetSchemaType()
        {
            if (Type.Enumerator != null)
                return Type.Enumerator;

            return null;
        }

        //String Oerators
        public static implicit operator DTDLBaseSchema(DTDLSchemaType type) => new DTDLBaseSchema { Type = type };
        public static implicit operator DTDLBaseSchema(string type) {
            var isParsed = Enum.TryParse<DTDLSchemaType>(type, true, out var result);
            Console.WriteLine(type);
            Console.WriteLine(isParsed);            
            

            if(isParsed)
                return result;

            return DTDLSchemaType.Unknown;  
        }

        //Should Serialize (Avoid empty objects during serialization)
        public bool ShouldSerializeDescription() => !Description.IsEmpty();
        public bool ShouldSerializeDisplayName() => !DisplayName.IsEmpty();
        public bool ShouldSerializeType() => !Type.IsEmpty();

    }

    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum DTDLSchemaType
    {
        // ordered according to DTDLv3 spec 
        [JsonIgnore]
        Unknown,

        [EnumMember(Value = "boolean")]
        Boolean,

        [EnumMember(Value = "date")]
        Date,

        [EnumMember(Value = "dateTime")]
        DateTime,

        [EnumMember(Value = "double")]
        Double,

        [EnumMember(Value = "duration")]
        Duration,

        [EnumMember(Value = "float")]
        Float,

        [EnumMember(Value = "integer")]
        Integer,

        [EnumMember(Value = "long")]
        Long,
        
        [EnumMember(Value = "string")]
        String,

        [EnumMember(Value = "time")]
        Time,

        [EnumMember(Value = "Enum")]
        Enum,

        [EnumMember(Value = "Array")]
        Array,

        [EnumMember(Value = "Map")]
        Map,

        [EnumMember(Value = "Object")]
        Object,

        [EnumMember(Value = "lineString")]
        LineString,

        [EnumMember(Value = "multiLineString")]
        MultiLineString,

        [EnumMember(Value = "multiPoint")]
        MultiPoint,

        [EnumMember(Value = "multiPolygon")]
        MultiPolygon,

        [EnumMember(Value = "point")]
        Point,

        [EnumMember(Value = "polygon")]
        Polygon,
    };

}
