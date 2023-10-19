using Newtonsoft.Json;
using WotConverterCore.Models.DigitalTwin.Schema;

namespace WotConverterCore.Models.DigitalTwin
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
