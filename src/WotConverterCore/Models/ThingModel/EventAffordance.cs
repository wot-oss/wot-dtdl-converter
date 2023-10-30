using Newtonsoft.Json;
using WotConverterCore.Extensions;
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

        //Should Serialize (Avoid empty objects during serialization)
        public bool ShouldSerializeSubscription() => !Subscription.IsEmpty();
        public bool ShouldSerializeData() => !Data.IsEmpty();
        public bool ShouldSerializeDataResponse() => !DataResponse.IsEmpty();
        public bool ShouldSerializeCancellation() => !Cancellation.IsEmpty();


    }
}
