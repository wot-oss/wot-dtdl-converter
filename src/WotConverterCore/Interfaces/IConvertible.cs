namespace WotConverterCore.Interfaces
{
    public interface IConvertible<K> : IGenericConvertible
    {
        void ConvertFrom(K value) { }
    }
}
