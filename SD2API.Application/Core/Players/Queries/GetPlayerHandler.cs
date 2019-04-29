using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SD2API.Application.Interfaces;

namespace SD2API.Application.Core.Players.Queries
{
    public class GetPlayerHandler : IRequestHandler<GetPlayer, GetPlayerResponse>
    {
        private IApiDbContext _apiDbContext;
        private IMapper _mapper;

        public GetPlayerHandler(IApiDbContext apiDbContext, IMapper mapper)
        {
            _apiDbContext = apiDbContext;
            _mapper = mapper;
        }

        public async Task<GetPlayerResponse> Handle(GetPlayer request, CancellationToken cancellationToken)
        {
            // to get the latest elo, we need to get the latest replay that has this player, and take the elo and level from the correlated player entry
            //var player = await _apiDbContext.ReplayHeaderPlayer
            //    .Where(x => x.PlayerUserId == request.PlayerUserId).OrderBy(x => x.ReplayHeaderId)

            var player = await _apiDbContext.ReplayHeaderPlayer
                //.Include(x => x.ReplayHeader)
                //.ThenInclude(x => x.Replay)
                .Where(x => x.PlayerUserId == request.PlayerUserId)
                .OrderByDescending(x => x.ReplayHeader.Replay.Date)
                .FirstOrDefaultAsync();

            if (player == null)
                return null;

            var playerAliases = await _apiDbContext.ReplayHeaderPlayer
                .Where(x => x.PlayerUserId == request.PlayerUserId)
                .Select(x => x.PlayerName)
                .Distinct()
                .ToListAsync();


            var response = _mapper.Map<GetPlayerResponse>(player);
            response.KnownPlayerNames = playerAliases;
            return response;


        }
    }
}
