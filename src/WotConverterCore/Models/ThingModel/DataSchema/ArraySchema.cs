using Newtonsoft.Json;
using WotConverterCore.Models.Common;

namespace WotConverterCore.Models.ThingModel.DataSchema
{
    public class ArraySchema : BaseDataSchema
    {
        public ArraySchema()
        {
            Type = TypeEnum.Array; 
        }

        [JsonProperty("items")]
        public List<BaseDataSchema> Items { get; set; }

        [JsonProperty("minItems")]
        public GenericStringInt? MinItems { get; set; }

        [JsonProperty("maxItems")]
        public GenericStringInt? MaxItems { get; set; }

    }
}
