using Newtonsoft.Json;
using WotConverterCore.Models.ThingModel.DataSchema;

namespace WotConverterCore.Models.ThingModel
{
    public class Event : BaseAffordance
    {
        public Event() : base() { }

        [JsonProperty("subscription")]
        public BaseDataSchema? Subscription { get; set; }

        [JsonProperty("data")]
        public BaseDataSchema? Data { get; set; }

        [JsonProperty("dataResponse")]
        public BaseDataSchema? DataResponse { get; set; }

        [JsonProperty("cancellation")]
        public BaseDataSchema? Cancellation { get; set; }
    }
}
