using static WotConverterCore.Models.Common.Constants.Strings;

namespace WotConverterCore.Models.Common.Exceptions
{
    public class InvalidDTDLSchema : Exception
    {
        public InvalidDTDLSchema() : base(InvalidDTDLSchemaMessage()) { }
    }
}
