using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AzureFunctionsV2.HttpExtensions.Annotations;
using AzureFunctionsV2.HttpExtensions.Authorization;
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
        [SwaggerResponse(200, typeof(GetReplayListResponse))]
        [SwaggerResponse(500, typeof(InternalServerErrorResponse))]
        [SwaggerResponse(400, typeof(InvalidRequestResponse))]
        [FunctionName("GetReplays")]
        public static async Task<IActionResult> GetReplays(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "replays")] HttpRequest req,
            [HttpQuery]HttpParam<int> skip,
            [HttpQuery]HttpParam<int> limit,
            [HttpQuery]HttpParam<string> orderBy,
            [HttpQuery]HttpParam<bool> descending,
            [HttpQuery]HttpParam<string> name,
            [HttpQuery]HttpParam<string> playerName,
            [HttpQuery]HttpParam<string> playerUserId,
            [HttpQuery]HttpParam<string> map,
            [HttpQuery]HttpParam<string> gameMode,
            [HttpQuery]HttpParam<int?> nbMaxPlayer,
            [HttpQuery]HttpParam<string> victoryCond,
            [HttpQuery]HttpParam<int?> incomeRate,
            [HttpQuery]HttpParam<int?> initMoney,
            [HttpQuery]HttpParam<bool?> rankedMatchesOnly,
            [Inject]IMediator mediator,
            ILogger log)
        {
            var result = await mediator.Send(new GetReplayList()
            {
                Descending = descending, Limit = limit, OrderBy = orderBy, Skip = skip, Query = new GetReplayList.ReplayQuery()
                {
                    GameMode = gameMode, IncomeRate = incomeRate, InitMoney = initMoney, Map = map, Name = name, NbMaxPlayer = nbMaxPlayer,
                    PlayerName = playerName, PlayerUserId = playerUserId, RankedMatchesOnly = rankedMatchesOnly, VictoryCond = victoryCond
                }
            });
            return new OkObjectResult(result);
        }

        [SwaggerResponse(200, typeof(GetReplayResponse))]
        [SwaggerResponse(404, null)]
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
        [Consumes("multipart/form-data")]
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

        [SwaggerResponse(200, typeof(DeleteReplayResponse))]
        [SwaggerResponse(404, null)]
        [SwaggerResponse(500, typeof(InternalServerErrorResponse))]
        [SwaggerResponse(401, null)]
        [HttpAuthorize(Scheme.Basic)]
        [FunctionName("DeleteReplay")]
        public static async Task<IActionResult> DeleteReplay(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "replay/{hash}")]
            HttpRequest req,
            string hash,
            [Inject] IMediator mediator,
            ILogger log)
        {
            var response = await mediator.Send(new DeleteReplay() {Hash = hash});
            return response.Success ? new OkObjectResult(response) : (IActionResult)new NotFoundResult();
        }
    }
}
