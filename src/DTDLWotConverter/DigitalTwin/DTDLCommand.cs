using DTDLWotConverter.DigitalTwin.Schema;
using Newtonsoft.Json;

namespace DTDLWotConverter.DigitalTwin
{
    public class DTDLCommand : DTDLBaseContent
    {
        public DTDLCommand() : base() { }

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
        public DTDLBaseSchema? Schema { get; set; }

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
        public DTDLBaseSchema? Schema { get; set; }

    }
}
