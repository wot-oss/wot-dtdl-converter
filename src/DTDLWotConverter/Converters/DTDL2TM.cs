using DTDLWotConverter.DigitalTwin;
using WotConverterCore.Models.ThingModel;

namespace DTDLWotConverter.Converters
{
    public class DTDL2TM
    {
        public static TM? DTDL2ThingModel(DTDL dtdl)
        {
            try
            {
                TM tm = new()
                {
                    Context = "https://www.w3.org/2019/wot/td/v1",
                    Title = dtdl.DisplayName,
                    Description = dtdl.Description ?? $"Creted from {dtdl.DisplayName} DTDL",
                };

                //DTDL Properties
                CreateTMProperties(ref tm, dtdl);

                //DTDL Commands
                CreateTMActions(ref tm, dtdl);

                //DTDL Telemetry
                CreateTMEvents(ref tm, dtdl);

                return tm;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        private static void CreateTMProperties(ref TM tm, DTDL dtdl) { }
        private static void CreateTMActions(ref TM tm, DTDL dtdl) { }
        private static void CreateTMEvents(ref TM tm, DTDL dtdl) { }
    }
}
