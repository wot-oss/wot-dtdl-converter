using Newtonsoft.Json;

namespace WotConverterCore.Models.DigitalTwin.Schema
{
    public class DTDLArraySchema : DTDLBaseSchema
    {
        public DTDLArraySchema(string elementSchema)
        {
            Type = DTDLSchemaType.Array;
            ElementSchema = elementSchema;
        }

        [JsonProperty("elementSchema")]
        public DTDLBaseSchema? ElementSchema { get; set; }

        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }

        public static implicit operator DTDLArraySchema(string elementSchema) => new DTDLArraySchema(elementSchema);

    }
}
