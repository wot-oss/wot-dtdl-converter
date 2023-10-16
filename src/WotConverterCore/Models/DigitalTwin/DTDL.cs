
namespace WotConverterCore.Models.DigitalTwin
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using WotConverterCore.Converters;
    using WotConverterCore.Interfaces;
    using WotConverterCore.Models;
    using WotConverterCore.Models.ThingModel;

    public class DTDL : BaseConvertible<DTDL>, IConvertible<TM>
    {
        [JsonProperty("@context")]
        public string Context { get; set; }

        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }

        [JsonProperty("contents")]
        private List<BaseDTDLContent>? Contents { get; set; }

        public void ConvertFrom(TM value)
        {
            var dtdl = DTDLConverters.ThingModel2DTDL(value);

            if (dtdl is null)
                return;

            Context = dtdl.Context;
            Id = dtdl.Id;
            Type = dtdl.Type;
            DisplayName = dtdl.DisplayName;
            Description = dtdl.Description;
            Comment = dtdl.Comment;
            Contents = dtdl.GetDTDLContents();
        }

        public List<BaseDTDLContent> GetDTDLContents(Func<BaseDTDLContent, bool>? query = null)
        {
            if (query != null)
                return Contents?.Where(query)?.ToList() ?? new();

            return Contents?.ToList() ?? new();

        }

        public void Addcontent(BaseDTDLContent content)
        {
            if (Contents == null)
                Contents = new List<BaseDTDLContent>();

            Contents.Add(content);
        }

    }

}