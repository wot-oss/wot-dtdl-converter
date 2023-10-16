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
        public Dictionary<string, BaseDataSchema>? Properties { get; set; }

        [JsonProperty("required")]
        public string[]? Required { get; set; }

    }
}
