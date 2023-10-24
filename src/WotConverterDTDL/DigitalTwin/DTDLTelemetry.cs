using Newtonsoft.Json;
using WotConverterCore.Extensions;
using WotConverterDTDL.DigitalTwin.Schema;

namespace WotConverterDTDL.DigitalTwin
{
    public class DTDLTelemetry : DTDLBaseContent
    {
        public DTDLTelemetry() : base() { }

        [JsonProperty("@type")]
        public string Type => "Telemetry";

        [JsonProperty("schema")]
        public DTDLBaseSchema? Schema { get; set; }

        //Should Serialize (Avoid empty objects during serialization)
        public bool ShouldSerializeSchema() => !Schema.IsEmpty();

    }
}
