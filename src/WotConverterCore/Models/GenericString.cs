using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotConverterCore.Models
{
    public class GenericString : JsonConverter
    {
        public string? String { get; set; }
        public List<string>? StringArray { get; set; }

        public static implicit operator GenericString(string String) => new GenericString { String = String };
        public static implicit operator GenericString(List<string> StringArray) => new GenericString { StringArray = StringArray };

        public static readonly GenericString Singleton = new GenericString();

        public override bool CanConvert(Type t) => t == typeof(GenericString);

        public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new GenericString { String = stringValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<string>>(reader);
                    return new GenericString { StringArray = arrayValue };
                default:
                    return new GenericString { };
            }
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            var value = (GenericString?)untypedValue ?? new GenericString();
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.StringArray != null)
            {
                serializer.Serialize(writer, value.StringArray);
                return;
            }

            return;
        }

    }
}
