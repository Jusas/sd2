using System;
using System.IO;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSwag;
using NSwag.Annotations;
using SD2API.Application.Core.Players.Queries;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace SD2API.FunctionAPI
{
    public static class Players
    {
        [SwaggerResponse(200, typeof(GetPlayerResponse))]
        [SwaggerResponse(404, null)]
        [FunctionName("GetPlayer")]
        public static async Task<IActionResult> GetPlayer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "players/{playerUserId}")] HttpRequest req,
            [Inject]IMediator mediator,
            string playerUserId,
            ILogger log)
        {
            var result = await mediator.Send(new GetPlayer() {PlayerUserId = playerUserId});
            return result == null ? (IActionResult)new NotFoundResult() : new OkObjectResult(result);
        }

        [SwaggerResponse(200, typeof(GetPlayerBattlegroupsResponse))]
        [SwaggerResponse(404, null)]
        [FunctionName("GetPlayerBattlegroups")]
        public static async Task<IActionResult> GetPlayerBattlegroups(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "players/{playerUserId}/battlegroups")] HttpRequest req,
            [Inject]IMediator mediator,
            string playerUserId,
            ILogger log)
        {
            var result = await mediator.Send(new GetPlayerBattlegroups() { PlayerUserId = playerUserId });
            return result == null ? (IActionResult)new NotFoundResult() : new OkObjectResult(result);
        }
    }
}
