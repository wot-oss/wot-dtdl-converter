namespace WotConverterCore.Models.Common
{
    public class GenericStringInt : IGenericString
    {

        private int? integer;
        private string? stringInt;

        public int? Integer { get { return integer; } set { stringInt = null; integer = value; } }
        public string? StringInt { get { return stringInt; } set { integer = null; stringInt = value; } }

        public static implicit operator GenericStringInt(int integer) => new GenericStringInt { Integer = integer };
        public static implicit operator GenericStringInt(string stringInt) => new GenericStringInt { StringInt = stringInt };

        public bool isString() => stringInt != null;

        public override string ToString()
        {
            if (integer != null)
                return integer.ToString();
            else if (stringInt != null)
                return stringInt.ToString();
            else
                return "";
        }
    }
}
