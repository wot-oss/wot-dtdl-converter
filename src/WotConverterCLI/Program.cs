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

        var pathToJsons = args[0].ToString();

        if (pathToJsons.IndexOfAny(Path.GetInvalidPathChars()) != -1)
        {
            Console.WriteLine("The first argument must be a valid accessible path");
            return 1;
        }


        var parameters = new ProgramParameters();

        if (args.Length > 1)
        {
            var arguments = args.Take(Range.StartAt(1)).ToArray();
            foreach (var arg in arguments)
                parameters.ParseFlag(arg);
        }

        string tmSchema = string.Empty;
        var dtdls = new Dictionary<string, DTDL>();
        var tms = new Dictionary<string, TM>();
        var files = new List<string>();

        var readDTDLs = 0;
        var readTms = 0;
        var convertedDTDLs = 0;
        var convertedTms = 0;
        var writtenDTDLs = 0;
        var writtenTMs = 0;
        var errorList = new Dictionary<string, string>();

        if (!((File.GetAttributes(pathToJsons) & FileAttributes.Directory) == FileAttributes.Directory))
        {
            files.Add(pathToJsons);
        }
        else
        {
            files = Directory.GetFiles(pathToJsons, "*", SearchOption.AllDirectories)?.ToList();
            if (!files?.Any(_ => _.EndsWith(".jsonld") || _.EndsWith(".json")) ?? false)
            {
                Console.WriteLine($"No valid Jsonld files contained in: {pathToJsons}");
                return 1;
            }

            files = files?.Where(_ => _.EndsWith(".jsonld") || _.EndsWith(".json")).ToList();
        }

        Console.WriteLine("=== Validating TMs...");

        foreach (var item in files ?? new())
        {
            try
            {
                using (var itmeStream = new StreamReader(item, Encoding.Latin1))
                {
                    var filename = Path.GetFileName(item);

                    var fileContent = itmeStream.ReadToEnd();
                    var jDocument = JObject.Parse(fileContent);

                    var type = jDocument["@type"];

                    if (type == null)
                        continue;

                    if (type.ToString().ToLowerInvariant() == "interface")
                    {
                        readDTDLs++;
                        Console.WriteLine($"\n--- DTDL {item}");

                        var dtdl = DTDL.Deserialize(fileContent);

                        if (dtdl == null)
                        {
                            Console.WriteLine($"Unable to parse DTDL {filename}");
                            continue;
                        }

                        dtdl.DisplayName ??= filename.Replace(".jsonld", "").Replace(".json", "");

                        var tm = dtdl.ConvertToTm(parameters.ConversionSettings);

                        if (tm != null)
                        {
                            tms.Add(item, tm);
                            convertedDTDLs++;
                        }
                    }

                    if (type.ToString().ToLowerInvariant() == "tm:thingmodel")
                    {
                        readTms++;
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

                        thingModel.Title ??= filename.Replace("jsonld", "").Replace(".json", "");

                        var dtdl = DTDL.ConvertFromTm(thingModel);

                        if (dtdl != null)
                        {
                            dtdls.Add(item, dtdl);
                            convertedTms++;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during conversion for file {item} => {ex.Message}");
                errorList.Add(item, ex.Message);
            }
        }


        bool exists = Directory.Exists(parameters.DtdlOutputPath);

        if (!exists)
            Directory.CreateDirectory(parameters.DtdlOutputPath);

        exists = Directory.Exists(parameters.TmOutputPath);

        if (!exists)
            Directory.CreateDirectory(parameters.TmOutputPath);

        if (dtdls.Any())
            Console.WriteLine("\n=== Writing DTDLS... \n");

        foreach (var dtdl in dtdls)
        {
            var trimmedFilePath = Path.GetDirectoryName(Path.GetRelativePath(pathToJsons, dtdl.Key)) ?? "";
            var filePath = Path.Combine(parameters.DtdlOutputPath, trimmedFilePath);
            var fileName = Path.GetFileName(dtdl.Key);

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(filePath);

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(filePath, fileName), false, Encoding.Latin1))
            {
                var serializeddtdl = dtdl.Value.Serialize();
                await outputFile.WriteAsync(serializeddtdl);
                Console.WriteLine("New DTDL file output: {0} \n ---", ((FileStream)outputFile.BaseStream).Name);
                writtenDTDLs++;
            }
        }

        if (tms.Any())
            Console.WriteLine("\n=== Writing TMS... \n");

        foreach (var tm in tms)
        {
            var trimmedFilePath = Path.GetDirectoryName(Path.GetRelativePath(pathToJsons, tm.Key)) ?? "";
            var filePath = Path.Combine(parameters.TmOutputPath, trimmedFilePath);
            var fileName = Path.GetFileName(tm.Key);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(filePath, fileName), false, Encoding.Latin1))
            {
                var serializedTm = tm.Value.Serialize();
                await outputFile.WriteAsync(serializedTm);
                Console.WriteLine("New TM file output: {0} \n ---", ((FileStream)outputFile.BaseStream).Name);
                writtenTMs++;

            }
        }

        Console.WriteLine("\nConversion Ended !");
        Console.WriteLine();

        Console.WriteLine($"- DTDLs Found: {readDTDLs}");
        Console.WriteLine($"- TMs Found: {readTms}");
        Console.WriteLine();

        Console.WriteLine($"- DTDLS successfully converted: {convertedDTDLs}");
        Console.WriteLine($"- TMs successfully converted: {convertedTms}");
        Console.WriteLine();

        Console.WriteLine($"- DTDLs successfully written: {writtenDTDLs}");
        Console.WriteLine($"- TMs successfully written: {writtenTMs}");
        Console.WriteLine();

        if (errorList.Any())
            Console.WriteLine("Errors: \n");

        foreach (var error in errorList)
        {
            Console.WriteLine($"{error.Key} => \t {error.Value} \n");
        }

        return 0;
    }


}


public class ProgramParameters
{

    public TMDefaultConversionParameters ConversionSettings { get; private set; } = new TMDefaultConversionParameters();
    public string TmOutputPath { get; private set; } = "./Thing-Models";
    public string DtdlOutputPath { get; private set; } = "./DTDLs";

    private (string? flag, string? value) GetFlagKeyAndValue(string argument)
    {
        var separated = argument.Split('=');

        if (separated.Length == 2)
            return (separated[0], separated[1]);

        return (null, null);
    }

    public void ParseFlag(string? argument)
    {
        (var flag, var value) = GetFlagKeyAndValue(argument);

        if (flag == null || value == null)
            return;

        switch (flag.ToLower())
        {
            case "-includehrefs":
                if (value == "true")
                    ConversionSettings.InsertHrefs = true;
                else
                    ConversionSettings.InsertHrefs = false;
                return;
            case "-tmoutpath":
                if (value.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                    return;
                TmOutputPath = value;
                return;
            case "-dtdloutpath":
                if (value.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                    return;
                DtdlOutputPath = value;
                return;
            default:
                return;
        }
    }
}