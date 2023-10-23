﻿using Newtonsoft.Json;
using System.Runtime.Serialization;
using WotConverterCore.Models.Common.Interfaces;
using WotConverterCore.Models.ThingModel.Serializers;

namespace WotConverterCore.Models.ThingModel.DataSchema
{
    public class BaseDataSchema : ISerializable<BaseDataSchemaSerializer>
    {
        [JsonProperty("type")]
        public TypeEnum? Type { get; protected set; }

        [JsonProperty("@type")]
        public string? LdType { get; set; }

        [JsonProperty("format")]
        public string? Format { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("titles")]
        public Dictionary<string, string>? Titles { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("descriptions")]
        public Dictionary<string, string>? Descriptions { get; set; }

        [JsonProperty("writeOnly")]
        public bool? WriteOnly { get; set; }

        [JsonProperty("readOnly")]
        public bool? Readonly { get; set; }

        [JsonProperty("const")]
        public bool? Const { get; set; }

        [JsonProperty("enum")]
        public List<string>? Enum { get; set; }


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
        Array

    };
}