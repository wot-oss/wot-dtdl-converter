using WotConverterCore.Models.Common.Interfaces;
using WotConverterCore.Models.Serializers;

namespace WotConverterCore.Models.Common
{
    public class GenericStringDouble : IGenericString, ISerializable<GenericStringDoubleSerializer>
    {

        private double? doubleValue;
        private string? stringDouble;
        public double? Double { get { return doubleValue; } set { stringDouble = null; doubleValue = value; } }

        public string? Stringdouble { get { return stringDouble; } set { doubleValue = null; stringDouble = value; } }

        public static implicit operator GenericStringDouble(double number) => new GenericStringDouble { Double = number };
        public static implicit operator GenericStringDouble(string stringDouble) => new GenericStringDouble { Stringdouble = stringDouble };

        internal static readonly GenericStringDoubleSerializer Serializer = new GenericStringDoubleSerializer();
        public bool isString() => stringDouble != null;

        public override string ToString()
        {
            if (doubleValue != null)
                return doubleValue.ToString();
            else if (stringDouble != null)
                return stringDouble.ToString();
            else
                return "";
        }

    }
}
