using Newtonsoft.Json;
using WotConverterCore.Extensions;
using WotConverterCore.Models.Common;
using WotConverterDTDL.DigitalTwin.Schema;

namespace WotConverterDTDL.DigitalTwin
{
    public class DTDLCommand : DTDLBaseContent
    {
        public DTDLCommand() : base() { }

        [JsonProperty("@type")]
        public GenericStringArray<string> Type { get; set; } = "Command";
        public DTDLCommandRequest? Request { get; set; }
        public DTDLCommandResponse? Response { get; set; }
    }

    public class DTDLCommandRequest
    {
        [JsonProperty("@type")]
        public GenericStringArray<string> Type => "CommandRequest";

        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public GenericStringDictionary<string>? DisplayName { get; set; }

        [JsonProperty("description")]
        public GenericStringDictionary<string>? Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }

        [JsonProperty("schema")]
        public DTDLBaseSchema? Schema { get; set; }

        [JsonProperty("unit")]
        public string? Unit { get; set; }
        
        //Should Serialize (Avoid empty objects during serialization)
        public bool ShouldSerializeSchema() => !Schema.IsEmpty();
        public bool ShouldSerializeDisplayName() => !DisplayName.IsEmpty();
        public bool ShouldSerializeDescription() => !Description.IsEmpty();
    }

    // TODO(Pedram): Why is this not inheriting from BaseContentSchema?
    public class DTDLCommandResponse
    {
        [JsonProperty("@type")]
        public GenericStringArray<string> Type => "CommandResponse";

        [JsonProperty("@id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public GenericStringDictionary<string>? DisplayName { get; set; }

        [JsonProperty("description")]
        public GenericStringDictionary<string>? Description { get; set; }

        [JsonProperty("comment")]
        public string? Comment { get; set; }

        [JsonProperty("schema")]
        public DTDLBaseSchema? Schema { get; set; }

        [JsonProperty("unit")]
        public string? Unit { get; set; }
        
        public bool ShouldSerializeSchema() => !Schema.IsEmpty();
        public bool ShouldSerializeDisplayName() => !DisplayName.IsEmpty();
        public bool ShouldSerializeDescription() => !Description.IsEmpty();

    }
}
