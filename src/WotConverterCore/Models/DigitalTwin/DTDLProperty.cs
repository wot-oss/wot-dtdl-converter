using Newtonsoft.Json;
namespace WotConverterCore.Models.DigitalTwin
{
    public class DTDLProperty : BaseDTDLContent
    {
        public DTDLProperty() : base() { }

        [JsonProperty("@type")]
        public string Type => "Property";

        [JsonProperty("writable")]
        public bool? Writable { get; set; }

        [JsonProperty("schema")]
        public string? Schema { get; set; }

    }
}
