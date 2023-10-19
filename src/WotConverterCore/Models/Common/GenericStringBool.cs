using WotConverterCore.Models.Common.Interfaces;
using WotConverterCore.Models.Serializers;

namespace WotConverterCore.Models.Common
{
    public class GenericStringBool : IGenericString, ISerializable<GenericStringBoolSerializer>
    {

        private bool? boolean;
        private string? stringBoolean;

        public bool? Bool { get { return boolean; } set { stringBoolean = null; boolean = value; } }
        public string? StringBool { get { return stringBoolean; } set { boolean = null; stringBoolean = value; } }

        public static implicit operator GenericStringBool(bool boolean) => new GenericStringBool { Bool = boolean };
        public static implicit operator GenericStringBool(string stringBool) => new GenericStringBool { StringBool = stringBool };

        public static readonly GenericStringBoolSerializer Serializer = new GenericStringBoolSerializer();
        public bool isString() => stringBoolean != null;

        public override string ToString()
        {
            if (boolean != null)
                return boolean.ToString();
            else if (stringBoolean != null)
                return stringBoolean.ToString();
            else
                return "";
        }
    }
}
