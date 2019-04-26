using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SD2API.Application.Core.Replays.Queries
{
    public class GetReplay : IRequest<GetReplayResponse>
    {
        public string Hash { get; set; }
    }
}
