using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WotConverterCore.Converters;
using WotConverterCore.Interfaces;

namespace WotConverterCore.Models
{
    public class ThingModel : BaseConvertible<ThingModel>, IConvertible<DTDL>
    {
        [JsonProperty("@context")]
        public GenericString Context { get; set; }

        [JsonProperty("@type")]
        public GenericString Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("id")]
        public string? Id { get; set; }
        
        [JsonProperty("titles")]
        public Dictionary<string, string>? Titles { get; set; }
        
        [JsonProperty("descriptions")]
        public Dictionary<string, string>?  Descriptions { get; set; }

        [JsonProperty("version")]
        public TmVersion? Version { get; set; }
        
        [JsonProperty("created")]
        public DateTimeOffset? Created { get; set; } = DateTimeOffset.Now;
        
        [JsonProperty("modified")]
        public DateTimeOffset? Modified { get; set; } = DateTimeOffset.Now;

        [JsonProperty("support")]
        public string? Support { get; set; }

        [JsonProperty("security")]
        public GenericString? Security { get; set; }

        [JsonProperty("securityDefinitions")]
        public Dictionary<string, SecurityScheme>? SecurityDefinitions { get; set; }

        [JsonProperty("links")]
        public Link[]? Links { get; set; }
        
        [JsonProperty("properties")]
        public Dictionary<string, Property>? Properties { get; set; }        
        
        [JsonProperty("events")]
        public Dictionary<string, Event>? Events { get; set; }        
        
        [JsonProperty("actions")]
        public Dictionary<string, Action>? Actions { get; set; }

        public void ConvertFrom(DTDL value)
        {
            var tm = ThingModelConverters.DTDL2ThingModel(value);
        }
    }

    public class TmVersion
    {
        [JsonProperty("instance")]
        public string Instance { get; set; }
    }

    public class SecurityScheme
    {

        [JsonProperty("scheme")]
        public string Scheme { get; set; }
    }

    public class Property
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("type")]
        public TypeEnum Type { get; set; }
        
        [JsonProperty("writeOnly")]
        public bool WriteOnly { get; set; }

        [JsonProperty("readOnly")]
        public bool Readonly { get; set; }

        [JsonProperty("observable")]
        public bool Observable { get; set; }

        [JsonProperty("forms")]
        public Form[] Forms { get; set; }

    }

    public class Event
    {

    }

    public class Action
    {

    }

    public class Form
    {

        [JsonProperty("op")]
        public Op[]? Op { get; set; }

        [JsonProperty("tm:ref")]
        public string TmRef { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("security")]
        public GenericString? Security { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        //Modbus Specific

        [JsonProperty("modbus:unitID")]
        public int? ModbusUnitId { get; set; }

        [JsonProperty("modbus:quantity")]
        public int? ModbusQuantity { get; set; }

        [JsonProperty("modbus:address")]
        public int? ModbusAddress { get; set; }
        
        [JsonProperty("modbus:pollingTime")]
        public int? ModbusPollingTime { get; set; }
        
        [JsonProperty("modbus:function")]
        public ModbusFunction? ModbusFunction { get; set; }

        [JsonProperty("modbus:entity")]
        public ModbusEntity? ModbusEntity { get; set; }
        
        [JsonProperty("modbus:zeroBasedAddressing")]
        public bool? ModbusZeroBaseaddressing { get; set; }
    }

    public class Link
    {
        [JsonProperty("anchor")]
        public string Anchor { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("hreflang")]
        public GenericString? Hreflang { get; set; }

        [JsonProperty("rel")]
        public string Rel { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("sizes")]
        public string Sizes { get; set; }
    }


    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum ModbusEntity
    {
        [EnumMember(Value = "HoldingRegister")]
        Holdingregister,

        [EnumMember(Value = "DiscreteInput")]
        DiscreteInput,

        [EnumMember(Value = "InputRegister")]
        InputRegister,
        
        [EnumMember(Value = "Coil")]
        Coil
    };
    
    
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum ModbusFunction : int
    {
        [EnumMember(Value = "readCoil")]
        Readcoil,

        [EnumMember(Value = "readDeviceVerification")]
        ReadDeviceVerification,

        [EnumMember(Value = "readDiscreteInput")]
        ReadDiscreteInput,

        [EnumMember(Value = "readHoldingRegisters")]
        ReadHoldingRegisters,

        [EnumMember(Value = "readInputRegisters")]
        ReadInputRegisters,
        
        [EnumMember(Value = "writeMultipleCoils")]
        WriteMultipleCoils,

        [EnumMember(Value = "writeMultipleHoldingRegisters")]
        WriteMultipleHoldingRegisters,

        [EnumMember(Value = "writeSingleCoil")]
        WriteSingleCoil,

        [EnumMember(Value = "writeSingleHoldingRegister")]
        WriteSingleHoldingRegister
    };

    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum Op
    {
        [EnumMember(Value = "observeproperty")]
        Observeproperty,

        [EnumMember(Value = "readproperty")]
        Readproperty
    };

    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum TypeEnum
    {
        [EnumMember(Value = "number")]
        Number ,
        
        [EnumMember(Value = "string")]
        String,
        
        [EnumMember(Value = "integer")]
        Integer,
        
        [EnumMember(Value = "boolean")]
        Boolean,
        
        [EnumMember(Value = "object")]
        Object
    };
}
