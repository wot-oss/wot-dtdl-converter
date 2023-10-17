using Newtonsoft.Json;
using WotConverterCore.Models.Common;

namespace WotConverterCore.Models.ThingModel.DataSchema
{
    public class NumberSchema : BaseDataSchema
    {
        public NumberSchema()
        {
            Type = TypeEnum.Number;
        }

        [JsonProperty("minimum")]
        public GenericStringDouble? Minimum { get; set; }

        [JsonProperty("exclusiveMinimum")]
        public GenericStringDouble? ExclusiveMinimum { get; set; }

        [JsonProperty("maximum")]
        public GenericStringDouble? Maximum { get; set; }

        [JsonProperty("exclusiveMaximum")]
        public GenericStringDouble? ExclusiveMaximum { get; set; }

        [JsonProperty("multipleOf")]
        public GenericStringDouble? MultipleOf { get; set; }
    }
}
