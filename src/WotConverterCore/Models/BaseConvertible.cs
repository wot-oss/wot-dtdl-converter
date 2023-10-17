using Newtonsoft.Json;
using System.Text.RegularExpressions;
using WotConverterCore.Interfaces;
using WotConverterCore.Models.Common;
using WotConverterCore.Models.DigitalTwin.Schema;
using WotConverterCore.Models.ThingModel;
using WotConverterCore.Models.ThingModel.DataSchema;

namespace WotConverterCore.Models
{
    public class BaseConvertible<T> : IGenericConvertible
    {
        protected static JsonSerializerSettings SerializationSettings = new JsonSerializerSettings
        {
            Converters =
            {
                GenericStringArray<string>.Serializer,
                GenericStringArray<OpEnum>.Serializer,
                GenericStringDouble.Serializer,
                GenericStringInt.Serializer,
                GenericStringBool.Serializer,
                BaseDataSchema.Serializer,
                DTDLBaseSchema.Serializer,
                Property.Serializer
            },
            NullValueHandling = NullValueHandling.Ignore
        };

        public static bool CanConvert<K>(K type)
        {
            var targetConvertible = typeof(IConvertible<>)
                .MakeGenericType(type?.GetType() ?? typeof(object));

            return targetConvertible.IsAssignableFrom(typeof(T));
        }

        public static string SanitizeMustacheJson(string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString))
                return inputString;

            //Replace '{{' with '"{{'
            inputString = Regex.Replace(inputString, @"(?<!""){1}(?<="":)(?:\s *)(\{\{)(?!""){1}", @"""{{");

            //Replace '}}' with '}}"'
            inputString = Regex.Replace(inputString, @"(?<!""){1}(\}\})(?=\s*,)(?!""){1}", @"}}""");

            return inputString;
        }
        public string Serialize() =>
            JsonConvert.SerializeObject(this, Formatting.Indented, SerializationSettings);

        public static T? Deserialize(string inputString, bool sanitizeMustache = false) =>
            JsonConvert.DeserializeObject<T>(
                sanitizeMustache ? SanitizeMustacheJson(inputString) : inputString, SerializationSettings);


    }
}




