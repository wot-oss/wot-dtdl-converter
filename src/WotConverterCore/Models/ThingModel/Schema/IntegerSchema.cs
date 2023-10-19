using Newtonsoft.Json;
using WotConverterCore.Models.Common;

namespace WotConverterCore.Models.ThingModel.DataSchema
{
    public class IntegerSchema : BaseDataSchema
    {
        public IntegerSchema()
        {
            Type = TypeEnum.Integer;
        }

        [JsonProperty("minimum")]
        public GenericStringInt? Minimum { get; set; }

        [JsonProperty("exclusiveMinimum")]
        public GenericStringInt? ExclusiveMinimum { get; set; }

        [JsonProperty("maximum")]
        public GenericStringInt? Maximum { get; set; }

        [JsonProperty("exclusiveMaximum")]
        public GenericStringInt? ExclusiveMaximum { get; set; }

        [JsonProperty("multipleOf")]
        public GenericStringInt? MultipleOf { get; set; }
    }
}
