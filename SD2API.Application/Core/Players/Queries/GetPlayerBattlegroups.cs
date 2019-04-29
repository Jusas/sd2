using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SD2API.Application.Core.Players.Queries
{
    public class GetPlayerBattlegroups : IRequest<GetPlayerBattlegroupsResponse>
    {
        public string PlayerUserId { get; set; }
    }
}
