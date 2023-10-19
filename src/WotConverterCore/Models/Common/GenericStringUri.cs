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
    public class GenericStringUri : IGenericString, ISerializable<GenericStringIntSerializer>
    {
        private Uri? uriValue;
        private string? stringUri;
        public Uri? Uri { get { return uriValue; } set { stringUri = null; uriValue = value; } }

        public string? StringUri { get { return stringUri; } set { uriValue = null; stringUri = value; } }

        public static implicit operator GenericStringUri(Uri uri) => new GenericStringUri { Uri = uri };
        public static implicit operator GenericStringUri(string stringUri) => new GenericStringUri { StringUri = stringUri };

        internal static readonly GenericStringUriSerializer Serializer = new GenericStringUriSerializer();
        public bool isString() => stringUri != null;

        public override string ToString()
        {
            if (uriValue != null)
                return uriValue.OriginalString.ToString();
            else if (stringUri != null)
                return stringUri;
            else
                return "";
        }
    }
}
