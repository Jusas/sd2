using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SD2API.Application.Interfaces;

namespace SD2API.Application.Core.Replays.Commands
{
    public class DeleteReplayHandler : IRequestHandler<DeleteReplay, DeleteReplayResponse>
    {
        private IApiDbContext _apiDbContext;

        public DeleteReplayHandler(IApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }
        public async Task<DeleteReplayResponse> Handle(DeleteReplay request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Hash))
                throw new ArgumentException("Hash must be a valid string");

            var match = await _apiDbContext.Replays.FirstOrDefaultAsync(x => x.ReplayHashStub == request.Hash);
            if (match == null)
                return new DeleteReplayResponse() {Success = false, Message = "Replay with this hash was not found"};

            _apiDbContext.Replays.Remove(match);
            await _apiDbContext.SaveChangesAsync(cancellationToken);
            return new DeleteReplayResponse() {Success = true, Message = "Deleted successfully"};
        }
    }
}
