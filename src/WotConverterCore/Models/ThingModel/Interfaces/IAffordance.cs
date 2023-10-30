using WotConverterCore.Models.Common;

namespace WotConverterCore.Models.ThingModel.Interfaces
{
    public interface IAffordance
    {
        string? LdType { get; set; }

        string? Title { get; set; }

        GenericStringDictionary<string>? Titles { get; set; }

        string? Description { get; set; }

        GenericStringDictionary<string>? Descriptions { get; set; }

        GenericStringArray<Form> Forms { get; set; }
    }
}
