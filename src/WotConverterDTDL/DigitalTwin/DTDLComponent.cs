using Newtonsoft.Json;
using WotConverterCore.Models.Common;

namespace WotConverterDTDL.DigitalTwin
{
    public class DTDLComponent : DTDLBaseContent
    {
        public DTDLComponent() : base() { }

        [JsonProperty("@type")]
        public GenericStringArray<string> Type { get; set; } = "Component";

        // IRI of interface type
        [JsonProperty("schema")]
        public string? Schema { get; set; }
    }
}
