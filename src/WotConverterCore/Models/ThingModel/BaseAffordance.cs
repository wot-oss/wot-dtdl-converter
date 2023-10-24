using Newtonsoft.Json;
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
        public Dictionary<string, string>? Titles { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("descriptions")]
        public Dictionary<string, string>? Descriptions { get; set; }

        [JsonProperty("forms")]
        public List<Form>? Forms { get; set; }
    }
}
