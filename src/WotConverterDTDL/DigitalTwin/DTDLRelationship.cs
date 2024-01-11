using Newtonsoft.Json;
using WotConverterCore.Models.Common;

namespace WotConverterDTDL.DigitalTwin
{
    public class DTDLRelationship : DTDLBaseContent
    {
        public DTDLRelationship() : base() { }

        [JsonProperty("@type")]
        public GenericStringArray<string> Type { get; set; } = "Relationship";

        [JsonProperty("writable")]
        public bool? Writable { get; set; }

        [JsonProperty("target")]
        public string? Target { get; set; }

        [JsonProperty("properties")]
        public List<DTDLProperty>? Properties { get; set; }

        [JsonProperty("maxMultiplicity")]
        public int? MaxMultiplicity { get; set; }

        [JsonProperty("minMultiplicity")]
        public int? MinMultiplicity { get; set; }
    }
}
