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

        //String Oerator 
        public static implicit operator DTDLObjectSchema(string stringRepresentation) => new DTDLObjectSchema { };
    }

    public class DTDLObjectField
    {
        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("@type")]
        public string Type => "Field";

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

        //Should Serialize (Avoid empty objects during serialization)
        public bool ShouldSerializeSchema() => !Schema.IsEmpty();
        public bool ShouldSerializeDisplayName() => !DisplayName.IsEmpty();
        public bool ShouldSerializeDescription() => !Description.IsEmpty();
    }
}
