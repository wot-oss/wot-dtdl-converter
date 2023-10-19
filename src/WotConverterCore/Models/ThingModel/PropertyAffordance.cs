using Newtonsoft.Json;
using WotConverterCore.Models.Common.Interfaces;
using WotConverterCore.Models.ThingModel.DataSchema;
using WotConverterCore.Models.ThingModel.Serializers;

namespace WotConverterCore.Models.ThingModel
{
    public class Property : BaseAffordance, ISerializable<PropertySerializer>
    {
        public Property() : base() { }

        [JsonProperty("observable")]
        public bool? Observable { get; set; }

        public BaseDataSchema? DataSchema { get; set; }

        internal static PropertySerializer Serializer = new PropertySerializer();

    }
}
