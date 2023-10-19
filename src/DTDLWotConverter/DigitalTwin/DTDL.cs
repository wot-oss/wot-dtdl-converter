using Newtonsoft.Json;
using WotConverterCore.Converters;
using WotConverterCore.Interfaces;
using WotConverterCore.Models.ThingModel;

namespace DTDLWotConverter.DigitalTwin
{
    public partial class DTDL : ITmConvertible<DTDL>
    {
        [JsonProperty("@context")]
        public string Context { get; set; }

        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; } = "Interface";

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }

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
            var dtdl = Tm2DTDL.ThingModel2DTDL(thingModel);

            if (dtdl is null)
                return null;

            return dtdl;
        }

        public TM ConvertToTm()
        {
            throw new NotImplementedException();
        }

    }

}