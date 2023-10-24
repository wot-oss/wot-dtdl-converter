using Newtonsoft.Json;

namespace WotConverterCore.Models.ThingModel.DataSchema
{
    public class ObjectSchema : BaseDataSchema
    {
        public ObjectSchema()
        {
            Type = TypeEnum.Object;
        }

        [JsonProperty("properties")]
        private Dictionary<string, BaseDataSchema>? Properties { get; set; }

        [JsonProperty("required")]
        public string[]? Required { get; set; }

        public Dictionary<string, BaseDataSchema>?  GetObjectProperties()
          => Properties;

        public void AddObjectProperty(KeyValuePair<string, BaseDataSchema> value)
        {
            if (Properties == null)
                Properties = new Dictionary<string, BaseDataSchema>();

            Properties.Add(value.Key, value.Value);
        }
    }
}
