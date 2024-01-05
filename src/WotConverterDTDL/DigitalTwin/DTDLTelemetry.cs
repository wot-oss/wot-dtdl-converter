using Newtonsoft.Json;
using WotConverterCore.Extensions;
using WotConverterCore.Models.Common;
using WotConverterDTDL.DigitalTwin.Schema;

namespace WotConverterDTDL.DigitalTwin
{
    public class DTDLTelemetry : DTDLBaseContent
    {
        public DTDLTelemetry() : base() { }

        [JsonProperty("@type")]
        public GenericStringArray<string> Type { get; set; } = "Telemetry";

        [JsonProperty("schema")]
        public DTDLBaseSchema? Schema { get; set; }

        [JsonProperty("unit")]
        public string? Unit { get; set; }        

        //Should Serialize (Avoid empty objects during serialization)
        public bool ShouldSerializeSchema() => !Schema.IsEmpty();

    }
}
