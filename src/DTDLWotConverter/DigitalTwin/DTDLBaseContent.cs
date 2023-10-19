using DTDLWotConverter.DigitalTwin.Serializers;
using Newtonsoft.Json;

namespace DTDLWotConverter.DigitalTwin
{
    public class DTDLBaseContent
    {
        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }

        internal static DTDLBaseContentSerializer Serializer = new DTDLBaseContentSerializer();

    }

}
