using Newtonsoft.Json;
using WotConverterCore.Models.Common;

namespace WotConverterDTDL.DigitalTwin.Schema
{
    public class DTDLMapSchema : DTDLBaseSchema
    {
        public DTDLMapSchema()
        {
            Type = DTDLSchemaType.Map;
        }
        
        [JsonProperty("mapKey")]
        public DTDLMapKey? MapKey { get; set; }
        
        [JsonProperty("mapValue")]
        public DTDLMapValue? MapValue { get; set; }

        public static implicit operator DTDLMapSchema(string stringRepresentation) => new DTDLMapSchema { };
    }

    public class DTDLMapKey
    {
        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("@type")]
        public string? Type => "Field";

        [JsonProperty("displayName")]
        public GenericStringDictionary<string>? DisplayName { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("description")]
        public GenericStringDictionary<string>? Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }

        // TODO(pedram): can only be string
        [JsonProperty("schema")]
        public DTDLBaseSchema? Schema { get; set; }

        [JsonProperty("unit")]
        public string? Unit { get; set; }
    }

    public class DTDLMapValue
    {
        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("@type")]
        public string? Type => "Field";

        [JsonProperty("displayName")]
        public GenericStringDictionary<string>? DisplayName { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("description")]
        public GenericStringDictionary<string>? Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }

        [JsonProperty("schema")]
        public DTDLBaseSchema? Schema { get; set; }

        [JsonProperty("unit")]
        public string? Unit { get; set; }
    }
};
