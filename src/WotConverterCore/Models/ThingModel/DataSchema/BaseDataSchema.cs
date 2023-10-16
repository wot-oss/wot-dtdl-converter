using Newtonsoft.Json;
using System.Runtime.Serialization;
using WotConverterCore.Models.Serializers;

namespace WotConverterCore.Models.ThingModel.DataSchema
{
    public class BaseDataSchema
    {
        [JsonProperty("@type")]
        public string? LdType { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("titles")]
        public Dictionary<string, string> Titles { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("descriptions")]
        public Dictionary<string, string> Descriptions { get; set; }

        [JsonProperty("type")]
        public TypeEnum Type { get; protected set; }

        [JsonProperty("writeOnly")]
        public bool? WriteOnly { get; set; }

        [JsonProperty("readOnly")]
        public bool? Readonly { get; set; }

        [JsonProperty("const")]
        public bool? Const { get; set; }

        internal static DataSchemaSerializer Serializer = new DataSchemaSerializer();
    }

    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum TypeEnum
    {
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
        Array

    };
}
