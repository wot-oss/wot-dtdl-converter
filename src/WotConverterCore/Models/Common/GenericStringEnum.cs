using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WotConverterCore.Models.Common.Interfaces;
using WotConverterCore.Models.Common.Serializers;
using WotConverterCore.Models.Serializers;

namespace WotConverterCore.Models.Common
{
    public class GenericStringEnum<T> : IGenericString, ISerializable<GenericStringIntSerializer> where T : struct
    {

        private T? enumerator;
        private string? stringEnum;

        public T? Enumerator { get { return enumerator; } set { stringEnum = null; enumerator = value; } }
        public string? StringEnumerator { get { return stringEnum; } set { enumerator = null; stringEnum = value; } }

        public static implicit operator GenericStringEnum<T>(T enumerator) => new GenericStringEnum<T> { Enumerator = enumerator };
        public static implicit operator GenericStringEnum<T>(string stringInt) => new GenericStringEnum<T> { StringEnumerator = stringInt };

        internal static readonly GenericStringEnumSerializer<T> Serializer = new GenericStringEnumSerializer<T>();

        public bool isString() => stringEnum != null;

        public override string ToString()
        {
            if (enumerator != null)
                return Enum.GetName(typeof(T), enumerator);
            else if (stringEnum != null)
                return stringEnum;
            else
                return "";
        }
    }
}
