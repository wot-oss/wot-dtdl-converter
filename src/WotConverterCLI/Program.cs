using Newtonsoft.Json.Linq;
using System.Text;
using WotConverterCore.Models.ThingModel;
using WotConverterDTDL.DigitalTwin;

internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        Console.WriteLine("======== WOT TD CONVERTER =======");

        if (args.Length < 0)
        {
            Console.WriteLine("Provide a valid TM file to convert!");
            return 1;
        }

        var pathToJsonLds = args[0];
        string tmSchema = string.Empty;

        var dtdls = new List<DTDL>();
        var tms = new List<TM>();
        var files = new List<string>();

        if (!((File.GetAttributes(pathToJsonLds) & FileAttributes.Directory) == FileAttributes.Directory))
        {
            files.Add(pathToJsonLds);
        }
        else
        {
            files = Directory.GetFiles(pathToJsonLds).ToList();
            if (!files.Any(_ => _.EndsWith(".jsonld")))
            {
                Console.WriteLine($"No valid Jsonld files contained in: {pathToJsonLds}");
                return 1;
            }

            files = files.Where(_ => _.EndsWith(".jsonld")).ToList();
        }

        Console.WriteLine("=== Validating TMs...");

        foreach (var item in files)
        {

            using (var itmeStream = new StreamReader(item, Encoding.Latin1))
            {
                var filename = Path.GetFileName(item);
                var fileContent = itmeStream.ReadToEnd();
                var jDocument = JObject.Parse(fileContent);

                var type = jDocument["@type"];

                if (type == null)
                    continue;

                if(type.ToString().ToLowerInvariant() == "interface")
                {
                    Console.WriteLine($"\n--- DTDL {item}");

                    var dtdl = DTDL.Deserialize(fileContent);

                    if (dtdl == null)
                    {
                        Console.WriteLine($"Unable to parse DTDL {filename}");
                        continue;
                    }

                    dtdl.DisplayName ??= filename.Replace("jsonld", "");

                    var tm = dtdl.ConvertToTm();

                    if (tm != null)
                        tms.Add(tm);
                }

                if (type.ToString().ToLowerInvariant() == "tm:thingmodel")
                {
                    Console.WriteLine($"\n--- TM {item}");

                    var istmValid = TM.Validate(fileContent);

                    if (!istmValid)
                    {
                        Console.WriteLine($"The file {filename} is not a valid TM !");
                        continue;
                    }

                    var thingModel = TM.Deserialize(fileContent);

                    if (thingModel == null)
                    {
                        Console.WriteLine($"Unable to parse TM  {filename}");
                        continue;
                    }

                    thingModel.Title ??= filename.Replace("jsonld", "");

                    var dtdl = DTDL.ConvertFromTm(thingModel);

                    if (dtdl != null)
                        dtdls.Add(dtdl);

                }
            }
        }

        bool exists = Directory.Exists("./dtdls");

        if (!exists)
            Directory.CreateDirectory("./dtdls");

        exists = Directory.Exists("./tms");

        if (!exists)
            Directory.CreateDirectory("./tms");

        if(dtdls.Any())
            Console.WriteLine("\n=== Writing DTDLS... \n");

        foreach (var dtdl in dtdls)
        {
            using (StreamWriter outputFile = new StreamWriter(
                Path.Combine("./dtdls" , $"{dtdl.Id?.Replace(":", "") ?? Guid.NewGuid().ToString()}.jsonld"), false, Encoding.Latin1))
            {
                var serializeddtdl = dtdl.Serialize();
                await outputFile.WriteAsync(serializeddtdl);
                Console.WriteLine("New DTDL file output: {0} \n ---", ((FileStream)outputFile.BaseStream).Name);
            }
        }   
        
        if(tms.Any())
            Console.WriteLine("\n=== Writing TMS... \n");

        foreach (var tm in tms)
        {
            using (StreamWriter outputFile = new StreamWriter(
                Path.Combine($"./tms", $"{tm.Title?.Replace(" ", "") ?? tm.Titles?.FirstOrDefault().Value ?? Guid.NewGuid().ToString()}.jsonld"), false, Encoding.Latin1))
            {
                var serializedTm = tm.Serialize();
                await outputFile.WriteAsync(serializedTm);
                Console.WriteLine("New TM file output: {0} \n ---", ((FileStream)outputFile.BaseStream).Name);
            }
        }

        Console.WriteLine("\nConversion Ended !");
        return 0;
    }
}