using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SD2API.Application.Interfaces;

namespace SD2API.Application.Core.Players.Queries
{
    public class GetPlayerBattlegroupsHandler : IRequestHandler<GetPlayerBattlegroups, GetPlayerBattlegroupsResponse>
    {
        private IApiDbContext _apiDbContext;

        public GetPlayerBattlegroupsHandler(IApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }
        public async Task<GetPlayerBattlegroupsResponse> Handle(GetPlayerBattlegroups request, CancellationToken cancellationToken)
        {
            var bgs = await _apiDbContext.ReplayHeaderPlayer.Where(x => x.PlayerUserId == request.PlayerUserId)
                .Select(x => x.PlayerDeckContent)
                .Distinct()
                .ToListAsync();

            if (!bgs.Any())
                return null;

            var response = new GetPlayerBattlegroupsResponse();
            response.AddRange(bgs);
            return response;
        }
    }
}
