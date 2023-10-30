using Newtonsoft.Json;
using WotConverterCore.Extensions;
using WotConverterCore.Models.Common;
using WotConverterCore.Models.ThingModel.Interfaces;

namespace WotConverterCore.Models.ThingModel
{
    public class BaseAffordance : IAffordance
    {
        [JsonProperty("@type")]
        public string? LdType { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("titles")]
        public GenericStringDictionary<string>? Titles { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("descriptions")]
        public GenericStringDictionary<string>? Descriptions { get; set; }

        [JsonProperty("forms")]
        public GenericStringArray<Form>? Forms { get; set; }

        //Should Serialize (Avoid empty objects during serialization)
        public bool ShouldSerializeTitles() => !Titles.IsEmpty();
        public bool ShouldSerializeDescriptions() => !Descriptions.IsEmpty();
        public bool ShouldSerializeFroms() => !Forms.IsEmpty();

    }
}
