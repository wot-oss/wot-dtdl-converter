using WotConverterCore.Models.ThingModel;

namespace WotConverterCore.Interfaces
{
    public interface IConvertible<K, T> where K : class where T : IConversionParameters
    {
        TM ConvertToTm(T? parameters);

        static K? ConvertFromTm(TM thingModel) => throw new NotImplementedException();

    }
}
