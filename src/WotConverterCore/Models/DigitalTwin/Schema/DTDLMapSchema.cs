namespace WotConverterCore.Models.DigitalTwin.Schema
{
    public class DTDLMapSchema : DTDLBaseSchema
    {
        public DTDLMapSchema()
        {
            Type = DTDLSchemaType.Map;
        }

        public static implicit operator DTDLMapSchema(string stringRepresentation) => new DTDLMapSchema { };
    }
}
