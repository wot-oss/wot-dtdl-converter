using WotConverterCore.Models.ThingModel;

namespace WotConverterCore.Interfaces
{
    public interface ITmConvertible<K> where K : class, new()
    {
        TM ConvertToTm();

        static K? ConvertFromTm(TM thingModel) => new();

    }
}
