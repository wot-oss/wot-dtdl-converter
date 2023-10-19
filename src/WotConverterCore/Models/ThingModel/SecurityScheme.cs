using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace WotConverterCore.Models.ThingModel
{
    public class SecurityScheme
    {
        [JsonProperty("scheme")]
        public SecuritySchemeEnum Scheme { get; set; }
    }

    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum SecuritySchemeEnum
    {
        [JsonIgnore]
        Unknown,

        [EnumMember(Value = "nosec")]
        noSecurityScheme,

        [EnumMember(Value = "auto")]
        AutoSecurityScheme,

        [EnumMember(Value = "combo")]
        ComboSecurityScheme,

        [EnumMember(Value = "basic")]
        BasicSecurityScheme,

        [EnumMember(Value = "digest")]
        DigestSecurityScheme,

        [EnumMember(Value = "apikey")]
        APIKeySecurityScheme,

        [EnumMember(Value = "bearer")]
        BearerSecurityScheme,

        [EnumMember(Value = "psk")]
        PSKSecurityScheme,

        [EnumMember(Value = "oauth2")]
        OAuth2SecurityScheme
    };

}
