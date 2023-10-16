using Newtonsoft.Json;
using WotConverterCore.Models.ThingModel.DataSchema;

namespace WotConverterCore.Models.ThingModel
{
    public class Action : BaseAffordance
    {
        public Action() : base() { }

        //Action Specific

        [JsonProperty("input")]
        public BaseDataSchema? Input { get; set; }

        [JsonProperty("output")]
        public BaseDataSchema? Output { get; set; }

        [JsonProperty("safe")]
        public bool? Safe { get; set; }

        [JsonProperty("idempotent")]
        public bool? Idempotent { get; set; }

        [JsonProperty("synchronous")]
        public bool? Synchronous { get; set; }
    }

}
