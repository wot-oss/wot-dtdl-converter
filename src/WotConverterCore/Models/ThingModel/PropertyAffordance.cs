using Newtonsoft.Json;
using WotConverterCore.Extensions;
using WotConverterCore.Models.ThingModel.DataSchema;
using WotConverterCore.Models.ThingModel.Serializers;

namespace WotConverterCore.Models.ThingModel
{
    public class Property : BaseAffordance
    {
        public Property() : base() { }

        [JsonProperty("observable")]
        public bool? Observable { get; set; }

        public BaseDataSchema? DataSchema { get; set; }

        //Should Serialize (Avoid empty objects during serialization)
        public bool ShouldSerializeDataSchema() => !DataSchema.IsEmpty();

    }
}
