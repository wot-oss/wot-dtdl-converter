using Newtonsoft.Json;
using WotConverterCore.Extensions;
using WotConverterCore.Models.Common;

namespace WotConverterCore.Models.ThingModel.DataSchema
{
    public class ObjectSchema : BaseDataSchema
    {
        public ObjectSchema()
        {
            Type = TypeEnum.Object;
        }

        [JsonProperty("properties")]
        private GenericStringDictionary<BaseDataSchema>? Properties { get; set; }

        [JsonProperty("required")]
        public string[]? Required { get; set; }

        public Dictionary<string, BaseDataSchema>?  GetObjectProperties()
          => Properties?.Dictionary;

        public void AddObjectProperty(KeyValuePair<string, BaseDataSchema> value)
        {
            if (Properties == null)
                Properties = new Dictionary<string, BaseDataSchema>();

            Properties.Add(value.Key, value.Value);
        }

        //Should Serialize (Avoid empty objects during serialization)
        public bool ShouldSerializeProperties() => !Properties.IsEmpty();
    }
}
