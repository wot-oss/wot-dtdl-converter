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


        //String Oerator 
        public static implicit operator DTDLArraySchema(string elementSchema) => new DTDLArraySchema(elementSchema);

        //Should Serialize (Avoid empty objects during serialization)
        public bool ShouldSerializeEleemntSchema() => !ElementSchema.IsEmpty();


    }
}
