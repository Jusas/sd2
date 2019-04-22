using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SD2API.Application.Core.Replays.Commands
{
    public class CreateReplay : IRequest<int>
    {
        public string Name { get; set; }
    }
}
