namespace WotConverterCore.Models.Common.Constants
{
    public static class Strings
    {
        private static string schemaPath => "WotConverterCore.Assets.Schema.Schema.jsonld";
        private static string invalidTmFileExceptionMessage => "Cannot use the provided TM file. Invalid TM JSONLD";
        public static string SchemaPath() => schemaPath;
        public static string InvalidTmFileExceptionMessage() => invalidTmFileExceptionMessage;

    }
}
