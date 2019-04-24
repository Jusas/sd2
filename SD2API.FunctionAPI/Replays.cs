using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AzureFunctionsV2.HttpExtensions.Annotations;
using AzureFunctionsV2.HttpExtensions.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSwag.Annotations;
using SD2API.Application.Core.Replays.Commands;
using SD2API.Application.Core.Replays.Queries;
using SD2API.FunctionAPI.Helpers;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace SD2API.FunctionAPI
{
    public static class Replays
    {
        [FunctionName("GetReplays")]
        public static async Task<IActionResult> GetReplays(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "replays")] HttpRequest req,
            [HttpQuery]HttpParam<int> intp,
            [Inject]IMediator mediator,
            ILogger log)
        {
            throw new NotImplementedException();
        }

        [FunctionName("GetReplay")]
        public static async Task<IActionResult> GetReplay(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "replays/{hash}")] HttpRequest req,
            string hash,
            [Inject]IMediator mediator,
            ILogger log)
        {
            var result = await mediator.Send(new GetReplay {Hash = hash});
            return result == null ? (IActionResult)new NotFoundResult() : new OkObjectResult(result);
        }

        [SwaggerResponse(201, typeof(CreateReplayResponse))]
        [SwaggerResponse(500, typeof(InternalServerErrorResponse))]
        [SwaggerResponse(400, typeof(InvalidRequestResponse))]
        [FunctionName("PostReplay")]
        public static async Task<IActionResult> PostReplay(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "replay")] HttpRequest req,
            [HttpForm(Required = true)]HttpParam<string> name,
            [HttpForm]HttpParam<string> description,
            [HttpForm(Required = true)]HttpParam<IFormFile> file,
            [Inject]IMediator mediator,
            ILogger log)
        {
            using (var fileStream = file.Value.OpenReadStream())
            {
                var replayData = new CreateReplay()
                {
                    Description = description,
                    Name = name,
                    File = new CreateReplay.UploadedFile(file.Value.FileName,
                        file.Value.ContentType,
                        fileStream)
                };
                var result = await mediator.Send(replayData);
                return new OkObjectResult(result);
            }
        }
    }
}
