using Newtonsoft.Json;
using System.Linq;
using System.Runtime.Serialization;
using WotConverterCore.Models.Common;
using WotConverterCore.Models.ThingModel.DataSchema;

namespace WotConverterCore.Models.ThingModel
{
    public partial class Form
    {
        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("op")]
        public GenericStringArray<OpEnum>? Op { get; set; }

        [JsonProperty("tm:ref")]
        public string? TmRef { get; set; }

        [JsonProperty("contentType")]
        public string? ContentType { get; set; }

        [JsonProperty("security")]
        public GenericStringArray<string>? Security { get; set; }

        [JsonProperty("scopes")]
        public GenericStringArray<string>? Scopes { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("uriVariables")]
        public Dictionary<string, BaseDataSchema>? UriVariables { get; set; }
        
        public bool HasOpProperty(string key)
        {
            var enumValue = Enum.Parse<OpEnum>(key, true);
            var searchInArray = Op?.Array?.Contains(enumValue);
            var searchInString = Op?.String != null ? Enum.Parse<OpEnum>(Op.String, true) == enumValue : false;
            return searchInArray ?? searchInString;
        }
        
        public bool HasOpProperty(OpEnum key)
        {
            var searchInArray = Op?.Array?.Contains(key);
            var searchInString = Op?.String != null ? Enum.Parse<OpEnum>(Op.String, true) == key : false;
            return searchInArray ?? searchInString;
        }
           
       
    }

    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum OpEnum
    {
        [EnumMember(Value = "observeproperty")]
        Observeproperty,

        [EnumMember(Value = "readproperty")]
        Readproperty,

        [EnumMember(Value = "writeproperty")]
        WriteProperty,

        [EnumMember(Value = "unobserveproperty")]
        UnobserveProperty,

        [EnumMember(Value = "invokeaction")]
        Invokeaction,

        [EnumMember(Value = "queryaction")]
        Queryaction,

        [EnumMember(Value = "cancelaction")]
        CancelAction,

        [EnumMember(Value = "subscribeevent")]
        Subscribeevent,

        [EnumMember(Value = "unsubscribeevent")]
        Unsubscribeevent,

        [EnumMember(Value = "readallproperties")]
        Readallproperties,

        [EnumMember(Value = "writeallproperties")]
        Writeallproperties,

        [EnumMember(Value = "readmultipleproperties")]
        Readmultipleproperties,

        [EnumMember(Value = "writemultipleproperties")]
        Writemultipleproperties,

        [EnumMember(Value = "observeallproperties")]
        Observeallproperties,

        [EnumMember(Value = "unobserveallproperties")]
        Unobserveallproperties,

        [EnumMember(Value = "subscribeallevents")]
        Subscribeallevents,

        [EnumMember(Value = "unsubscribeallevents")]
        Unsubscribeallevents,

        [EnumMember(Value = "queryallactions")]
        Queryallactions,

    };
}
