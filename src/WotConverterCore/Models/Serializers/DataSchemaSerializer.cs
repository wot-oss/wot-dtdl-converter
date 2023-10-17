﻿using Newtonsoft.Json;
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
            if (reader.TokenType == JsonToken.Null)
                return null;

            JObject jObject = JObject.Load(reader);

            var baseObject = new BaseDataSchema();
            var parsedType = jObject["type"]?.ToString();
            if (parsedType == null)
                return null;

            var baseObjectType = Enum.Parse(typeof(TypeEnum), parsedType, true);

            switch (baseObjectType)
            {
                case TypeEnum.Array:
                    baseObject = existingValue as ArraySchema ?? (ArraySchema)serializer.ContractResolver.ResolveContract(typeof(ArraySchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.Boolean:
                    baseObject = existingValue as BooleanSchema ?? (BooleanSchema)serializer.ContractResolver.ResolveContract(typeof(BooleanSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.Number:
                    baseObject = existingValue as NumberSchema ?? (NumberSchema)serializer.ContractResolver.ResolveContract(typeof(NumberSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.Integer:
                    baseObject = existingValue as IntegerSchema ?? (IntegerSchema)serializer.ContractResolver.ResolveContract(typeof(IntegerSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.Object:
                    baseObject = existingValue as ObjectSchema ?? (ObjectSchema)serializer.ContractResolver.ResolveContract(typeof(ObjectSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.String:
                    baseObject = existingValue as StringSchema ?? (StringSchema)serializer.ContractResolver.ResolveContract(typeof(StringSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case TypeEnum.Null:
                    baseObject = existingValue as NullSchema ?? (NullSchema)serializer.ContractResolver.ResolveContract(typeof(NullSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
                case null:
                default:
                    baseObject = existingValue as BaseDataSchema ?? (BaseDataSchema)serializer.ContractResolver.ResolveContract(typeof(BaseDataSchema)).DefaultCreator();
                    using (var subReader = jObject.CreateReader())
                        serializer.Populate(subReader, baseObject);
                    break;
            }

            using (var subReader = jObject.CreateReader())
                serializer.Populate(subReader, baseObject); 

            return baseObject;
        }

        public override void WriteJson(JsonWriter writer, object? untypedValue, JsonSerializer serializer)
        {
            serializer.Serialize(writer, untypedValue);
            return;
        }

        public override bool CanConvert(Type t) => t == typeof(BaseDataSchema);
    }
}
