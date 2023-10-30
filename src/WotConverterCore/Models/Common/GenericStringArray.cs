namespace WotConverterCore.Models.Common
{
    public class GenericStringArray<T> : IGenericString
    {
        private List<T>? array;
        private string? stringValue;

        public List<T>? Array { get { return array; } set { stringValue = null; array = value; } }
        public string? String { get { return stringValue; } set { array = null; stringValue = value; } }

        public static implicit operator GenericStringArray<T>(string String) => new GenericStringArray<T> { String = String };
        public static implicit operator GenericStringArray<T>(List<T> array) => new GenericStringArray<T> { Array = array };

        public void Add(T value)
        {
            if (array == null)
                array = new();

            array.Add(value);
        }

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
