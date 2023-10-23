using WotConverterCore.Models.ThingModel;

namespace WotConverterCore.Interfaces
{
    public interface IConvertible<K> where K : class
    {
        TM ConvertToTm();

        static K? ConvertFromTm(TM thingModel) => throw new NotImplementedException();

    }
}
