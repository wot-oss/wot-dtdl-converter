using Newtonsoft.Json;

namespace WotConverterCore.Models.DigitalTwin
{
    public class BaseDTDLContent
    {
        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }


    }

}
