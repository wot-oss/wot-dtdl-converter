
namespace WotConverterCore.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using WotConverterCore.Converters;
    using WotConverterCore.Interfaces;

    public class DTDL : BaseConvertible<DTDL>, IConvertible<ThingModel>
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
        public List<Content> Contents { get; set; }
        
        public void ConvertFrom(ThingModel value)
        {
            var dtdl = DTDLConverters.ThingModel2DTDL(value);

            Context = dtdl.Context;
            Id = dtdl.Id;
            Type = dtdl.Type;
            DisplayName = dtdl.DisplayName;
            Description = dtdl.Description;
            Comment = dtdl.Comment;
            Contents = dtdl.Contents;
        }

    }

    public class Content
    {
        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }

        [JsonProperty("schema")]
        public string Schema { get; set; }
    }

}