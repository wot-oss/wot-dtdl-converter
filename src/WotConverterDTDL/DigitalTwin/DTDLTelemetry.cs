using Newtonsoft.Json;
using WotConverterDTDL.DigitalTwin.Schema;

namespace WotConverterDTDL.DigitalTwin
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
