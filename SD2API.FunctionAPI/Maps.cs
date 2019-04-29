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
using SD2API.Application.Core.Maps.Queries;
using SD2API.Application.Core.Replays.Queries;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

namespace SD2API.FunctionAPI
{
    public static class Maps
    {
        [SwaggerResponse(200, typeof(GetMapListResponse))]
        [FunctionName("GetMaps")]
        public static async Task<IActionResult> GetMaps(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "maps")] HttpRequest req,
            [Inject]IMediator mediator,
            ILogger log)
        {
            var result = await mediator.Send(new GetMapList());
            return new OkObjectResult(result);
        }
    }
}
