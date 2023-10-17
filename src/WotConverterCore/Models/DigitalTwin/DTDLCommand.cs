using Newtonsoft.Json;

namespace WotConverterCore.Models.DigitalTwin
{
    public class DTDLCommand : BaseDTDLContent
    {
        public DTDLCommand() : base() {}

        [JsonProperty("@type")]
        public string Type => "Command";

        public DTDLCommandRequest? Request { get; set; }
        public DTDLCommandResponse? Response { get; set; }

    }

    public class DTDLCommandRequest
    {
        [JsonProperty("@type")]
        public string Type => "CommandRequest";

        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }

        [JsonProperty("schema")]
        public string? Schema { get; set; }

    }

    public class DTDLCommandResponse
    {
        [JsonProperty("@type")]
        public string Type => "CommandResponse";

        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }

        [JsonProperty("schema")]
        public string? Schema { get; set; }

    }
}
