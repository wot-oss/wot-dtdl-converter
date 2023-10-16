using WotConverterCore.Models.DigitalTwin;
using WotConverterCore.Models.ThingModel;
using WotConverterCore.Models.ThingModel.DataSchema;

namespace WotConverterCore.Converters
{
    internal static class DTDLConverters
    {
        public static DTDL? ThingModel2DTDL(TM tm)
        {

            try
            {
                DTDL dtdl = new()
                {
                    Context = "dtmi:dtdl:context;3",
                    Id = "dtmi:" + tm.Title.ToLowerInvariant().Replace(' ', ':') + ";1",
                    Type = "Interface",
                    DisplayName = tm.Title,
                    Description = tm.Description ?? $"Creted from {tm.Title} thing model",
                    Comment = tm.Base
                };

                //DTDL Properties
                var tmProperties = tm.GetProperties() ?? new();
                foreach (var property in tmProperties)
                {
                    foreach (Form form in property.Value.Forms)
                    {
                        DTDLProperty content = new();
                        content.Name = property.Key;
                        content.DisplayName = property.Value.Title;
                        content.Description = property.Value.Description;
                        content.Schema = GetDTDLType(property.Value.Type);

                        if (tm.Base.ToLower().StartsWith("modbus://"))
                        {
                            content.Comment = @$"modbus connection: function={form.ModbusFunction}, address={form.ModbusAddress}, quantity={form.ModbusQuantity}, unitId={form.ModbusUnitId}";
                        }
                        else
                        {
                            content.Schema = form.Type;
                        }

                        dtdl.Addcontent(content);
                    }
                }

                //DTDL Telemetry
                var tmActions = tm.GetActions() ?? new();
                foreach (var action in tmActions)
                {

                }

                //DTDL Telemetry
                var tmEvents = tm.GetEvents() ?? new();

                foreach (var eventValue in tmEvents)
                {

                }

                return dtdl;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        private static string GetDTDLType(TypeEnum TMType)
        {
            return "string";
        }
    }
}
