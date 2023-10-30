namespace WotConverterCore.Models.Common
{
    public class GenericStringDictionary<T> : IGenericString
    {

        private Dictionary<string, T>? dictionary = null;
        private string? stringValue = null;
        public Dictionary<string, T>? Dictionary { get { return dictionary; } set { stringValue = null; dictionary = value; } }

        public string? String { get { return stringValue; } set { dictionary = null; stringValue = value; } }

        public static implicit operator GenericStringDictionary<T>(Dictionary<string, T> dictionary) => new GenericStringDictionary<T> { Dictionary = dictionary };
        public static implicit operator GenericStringDictionary<T>(string value) => new GenericStringDictionary<T> { stringValue = value };
        public bool isString() => String != null; 

        public void Add(string key, T value)
        {
            if(dictionary == null)
                dictionary = new Dictionary<string, T>();

            dictionary.Add(key,value);
        }

        public override string ToString()
        {
            if (String != null)
                return String;
            if(Dictionary != null)
                return Dictionary.FirstOrDefault().Value?.ToString() ?? "";
            return "";
        }

    }
}
