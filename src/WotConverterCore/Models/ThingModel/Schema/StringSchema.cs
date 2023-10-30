using Newtonsoft.Json;
using WotConverterCore.Extensions;
using WotConverterCore.Models.Common;

namespace WotConverterCore.Models.ThingModel.DataSchema
{
    public class StringSchema : BaseDataSchema
    {
        public StringSchema()
        {
            Type = TypeEnum.String;
        }

        [JsonProperty("minLength")]
        public GenericStringInt? MinLength { get; set; }

        [JsonProperty("maxLength")]
        public GenericStringInt? MaxLength { get; set; }

        [JsonProperty("pattern")]
        public string? Pattern { get; set; }

        [JsonProperty("contentEncoding")]
        public string? ContentEncoding { get; set; }

        [JsonProperty("contentMediatype")]
        public string? contentMediatype { get; set; }

        //Should Serialize (Avoid empty objects during serialization)

        public bool ShouldSerializeMinLength() => !MinLength.IsEmpty();
        public bool ShouldSerializeMaxLength() => !MaxLength.IsEmpty();

    }
}
