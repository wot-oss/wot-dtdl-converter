using Newtonsoft.Json;
using WotConverterCore.Models.DigitalTwin.Schema;

namespace WotConverterCore.Models.DigitalTwin
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
