using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SD2API.Application.Interfaces;

namespace SD2API.Application.Core.Replays.Queries
{
    public class GetReplayHandler : IRequestHandler<GetReplay, GetReplayResponse>
    {
        private IApiDbContext _dbContext;
        private IMapper _mapper;

        public GetReplayHandler(IApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GetReplayResponse> Handle(GetReplay request, CancellationToken cancellationToken)
        {
            var replay = await _dbContext.Replays
                .Include(r => r.ReplayFooter)
                .ThenInclude(f => f.result)
                .Include(r => r.ReplayHeader)
                .ThenInclude(h => h.Game)
                .Include(r => r.ReplayHeader)
                .ThenInclude(h => h.Players)
                .FirstOrDefaultAsync(r => r.ReplayHashStub == request.Hash);

            if (replay == null)
                return null;

            return _mapper.Map<GetReplayResponse>(replay);            
        }
    }
}
