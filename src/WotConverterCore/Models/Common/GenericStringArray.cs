using System.Globalization;
using WotConverterCore.Models.Serializers;

namespace WotConverterCore.Models.Common
{
    public class GenericStringArray : IGenericString
    {
        private List<string>? stringArray;
        private string? stringValue;

        public List<string>? StringArray { get { return stringArray; } set { stringValue = null; stringArray = value; } }
        public string? String { get { return stringValue; } set { stringArray = null; stringValue = value; } }

        public static implicit operator GenericStringArray(string String) => new GenericStringArray { String = String };
        public static implicit operator GenericStringArray(List<string> StringArray) => new GenericStringArray { StringArray = StringArray };

        internal static readonly GenericStringArraySerializer Serializer = new GenericStringArraySerializer();

        public bool isString() => stringValue != null;
        public override string ToString()
        {
            if (stringValue != null)
                return stringValue.ToString();
            else
                return "";
        }
    }
}
