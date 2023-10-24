using Newtonsoft.Json;
using WotConverterCore.Extensions;
using WotConverterCore.Models.Common;
using WotConverterDTDL.DigitalTwin.Serializers;

namespace WotConverterDTDL.DigitalTwin
{
    public class DTDLBaseContent 
    {

        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("displayName")]
        public GenericStringDictionary? DisplayName { get; set; }

        [JsonProperty("description")]
        public GenericStringDictionary? Description { get; set; }
        
        [JsonProperty("comment")]
        public string? Comment { get; set; }

        //String Oerator
        public bool ShouldSerializeDescription() => !Description.IsEmpty();

        public bool ShouldSerializeDisplayName() => !DisplayName.IsEmpty();
    }

}
