using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SD2API.Application.Interfaces;
using SD2API.Domain;

namespace SD2API.Application.Core.Replays.Commands
{
    public class CreateReplayHandler : IRequestHandler<CreateReplay, int>
    {

        private IApiDbContext _apiDbContext;

        public CreateReplayHandler(IApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<int> Handle(CreateReplay request, CancellationToken cancellationToken)
        {
            var replay = new Replay()
            {
                Name = request.Name
            };
            _apiDbContext.Replays.Add(replay);
            await _apiDbContext.SaveChangesAsync(cancellationToken);

            return replay.ReplayId;
        }
    }
}
