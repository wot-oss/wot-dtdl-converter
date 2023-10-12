using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WotConverterCore.Models;

namespace WotConverterCore.Converters
{
    internal static class DTDLConverters
    {
        public static DTDL? ThingModel2DTDL(ThingModel tm)
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

                if (tm.Context.StringArray == null && tm.Context.String != null)
                {
                    if (tm.Context.String.Contains("/UA/"))
                    {
                        dtdl.Comment += ";" + tm.Context.String;
                    }
                }

                foreach (var context in tm.Context.StringArray ?? new List<string>())
                {
                    if (context.Contains("/UA/"))
                    {
                        dtdl.Comment += ";" + context;
                    }
                }

                if (dtdl.Comment == null)
                    throw new Exception("No Tm context has been provided.");
               

                dtdl.Contents = new List<Content>();
                foreach (KeyValuePair<string, Property> property in tm.Properties)
                {
                    foreach (Form form in property.Value.Forms)
                    {
                        Content content = new();
                        content.Name = property.Key + "/" + form.Href.Trim('/');
                        content.DisplayName = property.Key;
                        content.Type = "Telemetry";

                        if (tm.Base.ToLower().StartsWith("modbus://"))
                        {
                            content.Description = property.Key.Replace("_", "") + "_" + form.ModbusQuantity + ";" + form.ModbusAddress.ToString();
                            content.Schema = "string";
                        }
                        else
                        {
                            content.Description = property.Key;
                            content.Schema = form.Type;
                        }

                        dtdl.Contents.Add(content);
                    }
                }

                return dtdl;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

    }
}
