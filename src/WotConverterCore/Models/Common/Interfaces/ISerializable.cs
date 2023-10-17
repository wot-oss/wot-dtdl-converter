using Newtonsoft.Json;

namespace WotConverterCore.Models.Common.Interfaces
{
    public interface ISerializable<T> where T : JsonConverter
    {
        static T? Serializer { get; set; }
    }
}
