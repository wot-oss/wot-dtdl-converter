using Newtonsoft.Json;
using WotConverterCore.Extensions;
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

        //Should Serialize (Avoid empty objects during serialization)

        public bool ShouldSerializeMinimum() => !Minimum.IsEmpty();
        public bool ShouldSerializeExclusiveMinimum() => !ExclusiveMinimum.IsEmpty();
        public bool ShouldSerializeMaximum() => !Maximum.IsEmpty();
        public bool ShouldSerializeExclusiveMaximum() => !ExclusiveMaximum.IsEmpty();
        public bool ShouldSerializeMultipleOf() => !MultipleOf.IsEmpty();
    }
}
