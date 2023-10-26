using Newtonsoft.Json;
using WotConverterCore.Extensions;
using WotConverterCore.Interfaces;
using WotConverterCore.Models.Common;
using WotConverterCore.Models.ThingModel;
using WotConverterDTDL.Converters;

namespace WotConverterDTDL.DigitalTwin
{
    public partial class DTDL : IConvertible<DTDL>
    {
        [JsonProperty("@context")]
        public GenericStringArray<string>? Context { get; set; }

        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("@type")]
        public GenericStringArray<string> Type { get; set; } = "Interface";

        [JsonProperty("displayName")]
        public GenericStringDictionary? DisplayName { get; set; }

        [JsonProperty("description")]
        public GenericStringDictionary? Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }
        
        [JsonProperty("extends")]
        public GenericStringArray<string> ? Extends { get; set; }

        [JsonProperty("contents")]
        private List<DTDLBaseContent>? Contents { get; set; }

        public List<DTDLBaseContent> GetDTDLContents(Func<DTDLBaseContent, bool>? query = null)
        {
            if (query != null)
                return Contents?.Where(query)?.ToList() ?? new();

            return Contents?.ToList() ?? new();

        }

        public void Addcontent(DTDLBaseContent content)
        {
            if (Contents == null)
                Contents = new List<DTDLBaseContent>();

            Contents.Add(content);
        }

        public static DTDL? ConvertFromTm(TM thingModel)
        {
            var dtdl = TM2DTDL.ThingModel2DTDL(thingModel);

            if (dtdl is null)
                return null;

            return dtdl;
        }

        public TM ConvertToTm()
        {
            var tm = DTDL2TM.DTDL2ThingModel(this);

            if (tm is null)
                return null;

            return tm;
        }

        //Should Serialize (Avoid empty objects during serialization)

        public bool ShouldSerializeDisplayName() => !DisplayName.IsEmpty();
        public bool ShouldSerializeDescription() => !Description.IsEmpty();    
        public bool ShouldSerializeContext() => !Description.IsEmpty();
    }

}