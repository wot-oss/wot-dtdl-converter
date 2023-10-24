using Json.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using WotConverterCore.Extensions;
using WotConverterCore.Models.Common;
using WotConverterCore.Models.ThingModel.DataSchema;
using static WotConverterCore.Models.Common.Constants.Strings;

namespace WotConverterCore.Models.ThingModel
{
    public partial class TM
    {
        [JsonProperty("@context", Required = Required.Always)]
        public GenericStringArray<string> Context { get; set; } = string.Empty;

        [JsonProperty("@type", Required = Required.Always)]
        public GenericStringArray<string> LdType { get; set; } = string.Empty;

        [JsonProperty("schemaDefinitions")]
        public Dictionary<string, BaseDataSchema> SchemaDefinitions { get; set; } = new Dictionary<string, BaseDataSchema>();

        [JsonProperty("security")]
        public GenericStringArray<string> Security { get; set; } = new GenericStringArray<string>();

        [JsonProperty("securityDefinitions")]
        public Dictionary<string, SecurityScheme> SecurityDefinitions { get; set; } = new Dictionary<string, SecurityScheme>();

        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("base")]
        public string? Base { get; set; }

        [JsonProperty("id")]
        public Uri? Id { get; set; }

        [JsonProperty("titles")]
        public Dictionary<string, string>? Titles { get; set; }

        [JsonProperty("descriptions")]
        public Dictionary<string, string>? Descriptions { get; set; }

        [JsonProperty("version")]
        public TmVersion? Version { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset? Created { get; set; } = DateTimeOffset.Now;

        [JsonProperty("modified")]
        public DateTimeOffset? Modified { get; set; } = DateTimeOffset.Now;

        [JsonProperty("support")]
        public string? Support { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }

        [JsonProperty("properties")]
        private Dictionary<string, Property>? Properties { get; set; }

        [JsonProperty("actions")]
        private Dictionary<string, Action>? Actions { get; set; }

        [JsonProperty("events")]
        private Dictionary<string, Event>? Events { get; set; }

        public Dictionary<string, Property> GetProperties(Func<KeyValuePair<string, Property>, bool>? query = null)
        {
            if (query != null)
                return Properties?.Where(query)?.ToDictionary(_ => _.Key, _ => _.Value) ?? new();

            return Properties?.ToDictionary(_ => _.Key, _ => _.Value) ?? new();
        }

        public Dictionary<string, Action> GetActions(Func<KeyValuePair<string, Action>, bool>? query = null)
        {
            if (query != null)
                return Actions?.Where(query)?.ToDictionary(_ => _.Key, _ => _.Value) ?? new();

            return Actions?.ToDictionary(_ => _.Key, _ => _.Value) ?? new();
        }

        public Dictionary<string, Event> GetEvents(Func<KeyValuePair<string, Event>, bool>? query = null)
        {
            if (query != null)
                return Events?.Where(query)?.ToDictionary(_ => _.Key, _ => _.Value) ?? new();

            return Events?.ToDictionary(_ => _.Key, _ => _.Value) ?? new();
        }

        public void AddAction(string key, Action tmAction)
        {
            if (Actions == null)
                Actions = new Dictionary<string, Action>();

            if (!string.IsNullOrWhiteSpace(key))
                Actions.Add(key, tmAction);
        }

        public void AddProperty(string key, Property tmProperty)
        {
            if (Properties == null)
                Properties = new Dictionary<string, Property>();

            if (!string.IsNullOrEmpty(key))
                Properties.Add(key, tmProperty);
        }

        public void AddEvent(string key, Event tmEvent)
        {
            if (Events == null)
                Events = new Dictionary<string, Event>();

            Events.Add(key, tmEvent);
        }


        public static bool Validate(string thingModelJson)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream? stream = assembly.GetManifestResourceStream(SchemaPath()))
            using (StreamReader schemaSr = new StreamReader(stream))
            {
                var schema = JsonSchema.FromStream(schemaSr.BaseStream).Result;
                var deserializedTm = System.Text.Json.JsonDocument.Parse(thingModelJson);
                var res = schema.Evaluate(deserializedTm);

                if (!res.IsValid)
                    return false;

                return true;
            }
        }
    }

    public class TmVersion
    {
        [JsonProperty("instance")]
        public string Instance { get; set; }
    }


    public class Link
    {
        [JsonProperty("anchor")]
        public string? Anchor { get; set; }

        [JsonProperty("href")]
        public string? Href { get; set; }

        [JsonProperty("hreflang")]
        public GenericStringArray<string>? Hreflang { get; set; }

        [JsonProperty("rel")]
        public string? Rel { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("sizes")]
        public string? Sizes { get; set; }

        //Should Serialize (Avoid empty objects during serialization)
        public bool ShouldSerializeHreflang() => !Hreflang.IsEmpty();
    }

}


