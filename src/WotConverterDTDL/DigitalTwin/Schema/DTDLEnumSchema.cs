using Newtonsoft.Json;
using WotConverterCore.Extensions;
using WotConverterCore.Models.Common;

namespace WotConverterDTDL.DigitalTwin.Schema
{
    public class DTDLEnumSchema : DTDLBaseSchema
    {

        public DTDLEnumSchema() => Type = DTDLSchemaType.Enum;

        public DTDLEnumSchema(string valueSchema)
        {
            Type = DTDLSchemaType.Enum;
            ValueSchema = valueSchema;
        }

        [JsonProperty("valueSchema")]
        public string ValueSchema { get; set; }

        [JsonProperty("enumValues")]
        private List<DTDLEnumValue>? EnumValues { get; set; }

        public List<DTDLEnumValue>? GetEnumValues()
            => EnumValues;

        public void AddEnumValue(DTDLEnumValue value)
        {
            if (EnumValues == null)
                EnumValues = new List<DTDLEnumValue>();

            EnumValues.Add(value);
        }

        //String Oerator 
        public static implicit operator DTDLEnumSchema(string type)
        {
            if (type == null)
                throw new ArgumentNullException("DTDL Enum requires a valueSchema of type 'integer' or 'string' instead of null");

            if (type != "string" || type != "integer")
                throw new ArgumentException($"DTDL Enum requires a valueSchema of type 'integer' or 'string' instead of {type}");

            return new DTDLEnumSchema(type);
        }
    }

    public class DTDLEnumValue
    {
        [JsonProperty("@type")]
        public string Type => "EnumValue";

        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }

        [JsonProperty("enumValue")]
        public GenericStringInt? EnumValue { get; set; }
    }
}
