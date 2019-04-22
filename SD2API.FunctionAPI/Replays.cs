using System;
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
using SD2API.Application.Core.Replays.Commands;
using SD2API.Application.Core.Replays.Queries;
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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "replays/{id}")] HttpRequest req,
            int id,
            [Inject]IMediator mediator,
            ILogger log)
        {
            var result = await mediator.Send(new GetReplay {Id = id});
            return result == null ? (IActionResult)new NotFoundResult() : new OkObjectResult(result);
        }

        [FunctionName("PostReplay")]
        public static async Task<IActionResult> PostReplay(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "replay")] HttpRequest req,
            [HttpBody]HttpParam<CreateReplay> replay,
            [Inject]IMediator mediator,
            ILogger log)
        {
            var result = await mediator.Send(replay.Value);
            return new OkObjectResult(result);
        }
    }
}