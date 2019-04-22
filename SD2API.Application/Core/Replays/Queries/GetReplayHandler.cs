using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SD2API.Application.Interfaces;

namespace SD2API.Application.Core.Replays.Queries
{
    public class GetReplayHandler : IRequestHandler<GetReplay, GetReplayModel>
    {
        private IApiDbContext _dbContext;

        public GetReplayHandler(IApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetReplayModel> Handle(GetReplay request, CancellationToken cancellationToken)
        {
            var replay = await _dbContext.Replays.FindAsync(request.Id);
            if (replay == null)
                return null;

            return new GetReplayModel()
            {
                Name = replay.Name,
                ReplayId = replay.ReplayId
            };
        }
    }
}
