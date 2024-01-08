using Newtonsoft.Json;
using WotConverterCore.Extensions;
using WotConverterCore.Models.Common;

namespace WotConverterCore.Models.ThingModel.DataSchema
{
    public class ConstObject
    {
        public ConstObject() { }

        [JsonProperty("const")]
        public GenericStringInt? Const { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("titles")]
        public GenericStringDictionary<string>? Titles { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("descriptions")]
        public GenericStringDictionary<string>? Descriptions { get; set; }

        [JsonProperty("$comment")]
        public string? Comment { get; set; }

        public bool ShouldSerializeTitles() => !Titles.IsEmpty();
        public bool ShouldSerializeDescriptions() => !Descriptions.IsEmpty();
    }
}
