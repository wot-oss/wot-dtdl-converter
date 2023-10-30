namespace WotConverterCore.Models.Common
{
    public class GenericStringBool : IGenericString
    {
        private bool? boolean;
        private string? stringBoolean;

        public bool? Bool { get { return boolean; } set { stringBoolean = null; boolean = value; } }
        public string? StringBool { get { return stringBoolean; } set { boolean = null; stringBoolean = value; } }

        public static implicit operator GenericStringBool(bool boolean) => new GenericStringBool { Bool = boolean };
        public static implicit operator GenericStringBool(string stringBool) => new GenericStringBool { StringBool = stringBool };
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
