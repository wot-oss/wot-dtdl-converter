using Newtonsoft.Json;
using WotConverterCore.Extensions;
using WotConverterCore.Models.Common;

namespace WotConverterDTDL.DigitalTwin.Schema
{
    public class DTDLObjectSchema : DTDLBaseSchema
    {
        public DTDLObjectSchema()
        {
            Type = DTDLSchemaType.Object;
        }

        [JsonProperty("fields")]
        private List<DTDLObjectField>? Fields { get; set; }
        public List<DTDLObjectField>? GetObjectFields()
            => Fields;

        public void AddObjectField(DTDLObjectField value)
        {
            if (Fields == null)
                Fields = new List<DTDLObjectField>();

            Fields.Add(value);
        }

        public static implicit operator DTDLObjectSchema(string stringRepresentation) => new DTDLObjectSchema { };
    }

    public class DTDLObjectField
    {
        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("@type")]
        public string Type => "Field";

        [JsonProperty("displayName")]
        public GenericStringDictionary? DisplayName { get; set; }
        public bool ShouldSerializeDisplayName() => !DisplayName.IsEmpty();

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("description")]
        public GenericStringDictionary? Description { get; set; }
        public bool ShouldSerializeDescription() => !Description.IsEmpty();

        [JsonProperty("comment")]
        public string? Comment { get; set; }

        [JsonProperty("schema")]
        public DTDLBaseSchema? Schema { get; set; }
    }
}
