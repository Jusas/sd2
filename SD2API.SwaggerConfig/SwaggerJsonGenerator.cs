using System;
using System.Collections.Generic;
using System.Text;

namespace SD2API.SwaggerConfig
{
    public static class SwaggerJsonGenerator
    {
        public static string Generate()
        {
            var generator = new AzureFunctionsV2ToSwaggerGenerator(SwaggerConfiguration.SwaggerGeneratorSettings);
            var funcClasses = new[]
            {
                typeof(Replays),
                typeof(Maps),
                typeof(Players)
            };
            var document = await generator.GenerateForAzureFunctionClassesAsync(funcClasses, null);

            var json = document.ToJson();
        }
    }
}
