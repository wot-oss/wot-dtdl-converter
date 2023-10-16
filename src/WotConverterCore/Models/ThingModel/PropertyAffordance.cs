using Newtonsoft.Json;
using WotConverterCore.Models.ThingModel.DataSchema;
using WotConverterCore.Models.ThingModel.Interfaces;

namespace WotConverterCore.Models.ThingModel
{
    public class Property : BaseDataSchema, IAffordance
    {
        public Property() : base() {}

        //Interaction Affordance
        [JsonProperty("forms")]
        public List<Form> Forms { get; set; } = new List<Form>();

        //Property Specific
        [JsonProperty("observable")]
        public bool? Observable { get; set; } = false;
    }

}
