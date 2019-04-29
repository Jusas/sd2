using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SD2API.Application.Core.Replays.Commands
{
    public class DeleteReplay : IRequest<DeleteReplayResponse>
    {
        public string Hash { get; set; }
    }
}
