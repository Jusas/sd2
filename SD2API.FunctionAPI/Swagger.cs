using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using NSwag.Annotations;
using NSwag.SwaggerGeneration.AzureFunctionsV2;
using SD2API.Startup;

namespace SD2API.FunctionAPI
{
    public static class Swagger
    {
        /// <summary>
        /// Generates Swagger JSON.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [SwaggerIgnore]
        [FunctionName("swagger")]
        public static async Task<IActionResult> SwaggerEndpoint(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger")]
            HttpRequest req,
            ILogger log)
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
            return new OkObjectResult(json);
        }

        /// <summary>
        /// Serves SwaggerUI files.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="staticfile"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [SwaggerIgnore]
        [FunctionName("swaggerui")]
        public static async Task<IActionResult> SwaggerUi(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swaggerui/{staticfile}")] HttpRequest req,
            string staticfile,
            ILogger log)
        {
            var asm = Assembly.GetAssembly(typeof(Swagger));
            var files = asm.GetManifestResourceNames().Select(x => x.Replace("SD2API.FunctionAPI.SwaggerUi.", ""))
                .ToList();
            int index = -1;
            if ((index = files.IndexOf(staticfile)) != -1)
            {
                var fileExt = staticfile.Split('.').Last();
                var types = new Dictionary<string, string>()
                {
                    {"png", "image/png"},
                    {"html", "text/html"},
                    {"js", "application/javascript"},
                    {"css", "text/css"},
                    {"map", "application/json"}
                };
                var fileMime = types.ContainsKey(fileExt) ? types[fileExt] : "application/octet-stream";
                using (var stream = asm.GetManifestResourceStream(asm.GetManifestResourceNames()[index]))
                {
                    var buf = new byte[stream.Length];
                    await stream.ReadAsync(buf, 0, buf.Length);
                    return new FileContentResult(buf, fileMime);
                }
            }

            return new NotFoundResult();

        }

    }
}
