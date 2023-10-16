namespace WotConverterCore.Interfaces
{
    public interface IGenericConvertible
    {
        static bool CanConvert<K>(K type) => true;
    }
}
