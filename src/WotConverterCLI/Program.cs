using WotConverterCore.Models;
using System.IO;
using System.Reflection;
using Json.Schema;
using Newtonsoft.Json.Schema;
using Json.More;

Console.WriteLine("======== WOT TD CONVERTER =======");


if (args.Length < 0)
{
    Console.WriteLine("Provide a valid TM file to convert!");
    return 1;
}

var pathToTm = args[0];
string tmSchema = string.Empty;

var assembly = Assembly.GetExecutingAssembly();
var resourceName = "WotConverterCLI.Examples.Schema.Schema.jsonld";
var dtdls = new List<DTDL>();
var files = new List<string>();

if(!((File.GetAttributes(pathToTm) & FileAttributes.Directory) == FileAttributes.Directory))
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

Console.WriteLine("Validating TMs...");

foreach (var item in files)
{
    Console.WriteLine($"---------- TM {item} ----------");

    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
    using (StreamReader schemaSr = new StreamReader(stream))
    using (var tmSr = new StreamReader(item))
    {
        var sourceTm = tmSr.ReadToEnd();
        var schema = await Json.Schema.JsonSchema.FromStream(schemaSr.BaseStream);
        if (schema == null)
        {
            Console.WriteLine("Unable to parse source schema validation! ");
            return 1;

        }
        var deserializedTm = System.Text.Json.JsonDocument.Parse(sourceTm);
        var res = schema.Evaluate(deserializedTm);

        if (!res.IsValid)
        {
            Console.WriteLine("The TM file provided is not a valid json schema! ");
            return 1;
        }

        Console.WriteLine("Valid Tm File!");

        var thingModel = ThingModel.Deserialize(sourceTm);
        if (thingModel == null)
        {
            Console.WriteLine("Unable to convert TM to DTDL");
            return 1;
        }

        Console.WriteLine("TM properties: {0}", thingModel.Properties?.Count() ?? 0);

        var dtdl = new DTDL();
        dtdl.ConvertFrom(thingModel);

        dtdls.Add(dtdl);
    }
}


bool exists = Directory.Exists("./dtdls");

if (!exists)
    Directory.CreateDirectory("./dtdls");

foreach (var dtdl in dtdls)
{
    using (StreamWriter outputFile = new StreamWriter(Path.Combine("./dtdls", dtdl.Id.Replace(":", "") + ".jsonld")))
    {
        await outputFile.WriteAsync(dtdl.Serialize());
        Console.WriteLine("New DTDL file output: {0}", ((FileStream)(outputFile.BaseStream)).Name);
    }
}


return 0;

