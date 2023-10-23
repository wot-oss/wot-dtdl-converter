using Newtonsoft.Json;
using WotConverterCore.Extensions;
using WotConverterCore.Models.Common;

namespace WotConverterDTDL.DigitalTwin.Schema
{
    public class DTDLArraySchema : DTDLBaseSchema
    {
        public DTDLArraySchema()
        {
            Type = DTDLSchemaType.Array;
        }
        public DTDLArraySchema(string elementSchema)
        {
            Type = DTDLSchemaType.Array;
            ElementSchema = elementSchema;
        }

        [JsonProperty("elementSchema")]
        public DTDLBaseSchema? ElementSchema { get; set; }

        [JsonProperty("displayName")]
        public GenericStringDictionary? DisplayName { get; set; }
        public bool ShouldSerializeDisplayName() => !DisplayName.IsEmpty();

        [JsonProperty("description")]
        public GenericStringDictionary? Description { get; set; }
        public bool ShouldSerializeDescription() => !Description.IsEmpty();


        [JsonProperty("comment")]
        public string? Comment { get; set; }

        public static implicit operator DTDLArraySchema(string elementSchema) => new DTDLArraySchema(elementSchema);

    }
}
