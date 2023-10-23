using Newtonsoft.Json;
using WotConverterDTDL.DigitalTwin.Schema;

namespace WotConverterDTDL.DigitalTwin
{
    public class DTDLProperty : DTDLBaseContent
    {
        public DTDLProperty() : base() { }

        [JsonProperty("@type")]
        public string Type => "Property";

        [JsonProperty("writable")]
        public bool? Writable { get; set; }

        [JsonProperty("schema")]
        public DTDLBaseSchema? Schema { get; set; }

    }
}
