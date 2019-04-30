using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NSwag;
using NSwag.CodeGeneration.TypeScript;
using SD2API.FunctionAPI;
using SD2API.Startup;

namespace SD2API.TypescriptClientGenerator
{
    class Program
    {
        // First arg indicates what file we should save the generated code to.
        static void Main(string[] args)
        {
            if(args.Length != 1)
                throw new Exception("Expected exactly one argument, which is the target file name of the generated TS code.");

            var settings = new SwaggerToTypeScriptClientGeneratorSettings
            {
                ClassName = "SD2{controller}Api",
                GenerateClientInterfaces = true,
                Template = TypeScriptTemplate.Angular
            };

            var swaggerDoc = Swagger.GenerateSwaggerDocument().Result;            
            var generator = new SwaggerToTypeScriptClientGenerator(swaggerDoc, settings);
            var typescriptCode = generator.GenerateFile();

            File.WriteAllText(args[0], typescriptCode);
        }
    }
}
