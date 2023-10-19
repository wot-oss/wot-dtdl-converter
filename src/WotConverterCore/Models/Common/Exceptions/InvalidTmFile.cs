using static WotConverterCore.Models.Common.Constants.Strings;

namespace WotConverterCore.Models.Common.Exceptions
{
    public class InvalidTmFile : Exception
    {
        public InvalidTmFile() : base(InvalidTmFileExceptionMessage()) { }
    }
}
