# Wot-Converter
A two way converter for TM files 

# Project structure
The application is a .NET 6.0  solution composed of three main sub-projects:
1. **WoTConverterCore** - This project contains all the models for a Thing Model serialization/deserialization and validation. The project also contains all the JSONConverters used for a clean des/ser process.
2. **WotConverterCLI** - This is a simple entrypoint CLI application used to convert Thing Models or DTDLs in batches. the command simply takes a path to a file or a folder containing the target files to convert.
   The conversion automatically detects the type of file (DTLD or TM) by looking at the root "@type" property of the jsonlds it reads:
   - @typ="Interface." => DTDL
   - @type="tm:thingModel" => TM    
3. **WotConverterDTDL** - This project contains a DTDL model with the converters needed for the two ways conversion to Thing Models.


TODO: 
- The thing Model structure is not completed yet. There are parts in every affordance that are missing.
- DTDL's *Component*, *Relationship* properties are not mapped yet between TMs and DTDLs
- *GeoSpatial* and *Map* types are not mapped yet between TMs and DTDLs
- The *extends* property is not mapped yet between TMs and DTDLs
