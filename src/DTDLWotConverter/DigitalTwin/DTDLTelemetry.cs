using DTDLWotConverter.DigitalTwin.Schema;
using Newtonsoft.Json;

namespace DTDLWotConverter.DigitalTwin
{
    public class DTDLTelemetry : DTDLBaseContent
    {
        public DTDLTelemetry() : base() { }

        [JsonProperty("type")]
        public string Type => "Telemetry";

        [JsonProperty("schema")]
        public DTDLBaseSchema? Schema { get; set; }

    }
}
