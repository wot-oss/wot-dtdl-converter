using Newtonsoft.Json;
using System.Runtime.Serialization;
using WotConverterCore.Extensions;
using WotConverterCore.Models.Common;
using WotConverterCore.Models.ThingModel.Serializers;

namespace WotConverterCore.Models.ThingModel.DataSchema
{
    public class BaseDataSchema 
    {
        [JsonProperty("type")]
        public TypeEnum? Type { get; protected set; }

        [JsonProperty("$ref")]
        public string? Ref { get; set; }

        [JsonProperty("@type")]
        public string? LdType { get; set; }

        [JsonProperty("format")]
        public string? Format { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("titles")]
        public GenericStringDictionary<string>? Titles { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("descriptions")]
        public GenericStringDictionary<string>? Descriptions { get; set; }

        [JsonProperty("$comment")]
        public string? Comment { get; set; }

        [JsonProperty("unit")]
        public string? Unit { get; set; }

        [JsonProperty("writeOnly")]
        public bool? WriteOnly { get; set; }

        [JsonProperty("readOnly")]
        public bool? Readonly { get; set; }

        [JsonProperty("const")]
        public bool? Const { get; set; }

        [JsonProperty("enum")]
        public GenericStringArray<string>? Enum { get; set; }

        //Should Serialize (Avoid empty objects during serialization)
        public bool ShouldSerializeTitles() => !Titles.IsEmpty();
        public bool ShouldSerializeDescriptions() => !Descriptions.IsEmpty();
    }

    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum TypeEnum
    {
        [JsonIgnore]
        Unknown,

        [EnumMember(Value = "number")]
        Number,

        [EnumMember(Value = "string")]
        String,

        [EnumMember(Value = "integer")]
        Integer,

        [EnumMember(Value = "boolean")]
        Boolean,

        [EnumMember(Value = "object")]
        Object,

        [EnumMember(Value = "null")]
        Null,

        [EnumMember(Value = "array")]
        Array,

        [EnumMember(Value = "$ref")]
        Ref,
        
        // GeoJSON
        [EnumMember(Value = "https://geojson.org/schema/LineString.json")]
        LineString,
        
        [EnumMember(Value = "https://geojson.org/schema/MultiLineString.json")]
        MultiLineString,

        [EnumMember(Value = "https://geojson.org/schema/MultiPoint.json")]
        MultiPoint,

        [EnumMember(Value = "https://geojson.org/schema/MultiPolygon.json")]
        MultiPolygon,

        [EnumMember(Value = "https://geojson.org/schema/Point.json")]
        Point,
        
        [EnumMember(Value = "https://geojson.org/schema/Polygon.json")]
        Polygon
    };
}
