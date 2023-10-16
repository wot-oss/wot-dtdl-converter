namespace WotConverterCore.Models.ThingModel.Interfaces
{
    public interface IAffordance
    {
        string? LdType { get; set; }

        string Title { get; set; }

        Dictionary<string, string> Titles { get; set; }

        string Description { get; set; }

        Dictionary<string, string> Descriptions { get; set; }

        List<Form> Forms { get; set; }
    }
}
