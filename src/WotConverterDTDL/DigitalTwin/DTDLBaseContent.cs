using Newtonsoft.Json;
using WotConverterCore.Extensions;
using WotConverterCore.Models.Common;
using WotConverterCore.Models.Common.Interfaces;
using WotConverterDTDL.DigitalTwin.Serializers;

namespace WotConverterDTDL.DigitalTwin
{
    public class DTDLBaseContent : ISerializable<DTDLBaseContentSerializer>
    {

        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("displayName")]
        public GenericStringDictionary? DisplayName { get; set; }
        public bool ShouldSerializeDisplayName() => !DisplayName.IsEmpty();


        [JsonProperty("description")]
        public GenericStringDictionary? Description { get; set; }
        public bool ShouldSerializeDescription() => !Description.IsEmpty();


        [JsonProperty("comment")]
        public string? Comment { get; set; }


    }

}
