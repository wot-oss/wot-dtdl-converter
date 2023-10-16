using Newtonsoft.Json;

namespace WotConverterCore.Models.DigitalTwin
{
    public class DTDLTelemetry : BaseDTDLContent
    {
        public DTDLTelemetry() : base() { }

        [JsonProperty("type")]
        public string Type => "Telemetry";

        [JsonProperty("schema")]
        public string? Schema { get; set; }

    }
}
