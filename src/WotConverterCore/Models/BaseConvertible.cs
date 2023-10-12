using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WotConverterCore.Interfaces;

namespace WotConverterCore.Models
{
    public class BaseConvertible<T> : IGenericConvertible
    {
        protected static JsonSerializerSettings SerializationSettings = new JsonSerializerSettings
        {
            Converters =
            {
                GenericString.Singleton
            },
            NullValueHandling = NullValueHandling.Ignore
        };

        public static bool CanConvert<K>(K type)
        {
            var targetConvertible = typeof(IConvertible<>)
                .MakeGenericType(type?.GetType() ?? typeof(object));

            return targetConvertible.IsAssignableFrom(typeof(T));
        }

        public string Serialize() =>
            JsonConvert.SerializeObject(this, Formatting.Indented, SerializationSettings);

        public static T? Deserialize(string inputString) =>
             JsonConvert.DeserializeObject<T>(inputString, SerializationSettings);

        
    }
}
