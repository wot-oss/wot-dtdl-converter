using DTDLWotConverter.DigitalTwin;
using Newtonsoft.Json;
using WotConverterCore.Models.ThingModel;

Console.WriteLine("======== WOT TD CONVERTER =======");

if (args.Length < 0)
{
    Console.WriteLine("Provide a valid TM file to convert!");
    return 1;
}

var pathToTm = args[0];
string tmSchema = string.Empty;

var dtdls = new List<DTDL>();
var files = new List<string>();

if (!((File.GetAttributes(pathToTm) & FileAttributes.Directory) == FileAttributes.Directory))
{
    files.Add(pathToTm);
}
else
{
    files = Directory.GetFiles(pathToTm).ToList();
    if (!files.Any(_ => _.EndsWith(".jsonld")))
    {
        Console.WriteLine($"No valid Jsonld files contained in: {pathToTm}");
        return 1;
    }

    files = files.Where(_ => _.EndsWith(".jsonld")).ToList();
}

Console.WriteLine("=== Validating TMs...");

foreach (var item in files)
{
    Console.WriteLine($"\n--- TM {item}");

    using (var itmeStream = new StreamReader(item))
    {
        var filename = Path.GetFileName(item);
        var fileContent = itmeStream.ReadToEnd();
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

bool exists = Directory.Exists("./dtdls");

if (!exists)
    Directory.CreateDirectory("./dtdls");

Console.WriteLine("\n=== Writing DTDLS... \n");

foreach (var dtdl in dtdls)
{
    using (StreamWriter outputFile = new StreamWriter(Path.Combine("./dtdls", dtdl.Id.Replace(":", "") + ".jsonld")))
    {
        await outputFile.WriteAsync(dtdl.Serialize());
        Console.WriteLine("New DTDL file output: {0} \n ---", ((FileStream)(outputFile.BaseStream)).Name);
    }
}

Console.WriteLine("\nConversion Ended !");
return 0;

