using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using WotConverterCore.Models.Common;
using WotConverterCore.Models.ThingModel.DataSchema;

namespace WotConverterCore.Models.Serializers
{
    internal class DataSchemaSerializer : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
        {
            var baseDataSchema = serializer.Deserialize<BaseDataSchema>(reader);

            if (baseDataSchema == null)
                return null;

            switch (baseDataSchema.Type)
            {
                case TypeEnum.Array:
                    return serializer.Deserialize<ArraySchema>(reader);
                case TypeEnum.Boolean:
                    return serializer.Deserialize<BooleanSchema>(reader);
                case TypeEnum.Number:
                    return serializer.Deserialize<NumberSchema>(reader);
                case TypeEnum.Integer:
                    return serializer.Deserialize<IntegerSchema>(reader);
                case TypeEnum.Object:
                    return serializer.Deserialize<ObjectSchema>(reader);
                case TypeEnum.String:
                    return serializer.Deserialize<StringSchema>(reader);
                case TypeEnum.Null:
                    return serializer.Deserialize<NullSchema>(reader);
                default:
                    break;
            }

            return baseDataSchema;
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            serializer.Serialize(writer, untypedValue);
            return;
        }

        public override bool CanConvert(Type t) => t == typeof(BaseDataSchema);
    }
}
