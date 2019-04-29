using MediatR;

namespace SD2API.Application.Core.Players.Queries
{
    public class GetPlayer : IRequest<GetPlayerResponse>
    {
        public string PlayerUserId { get; set; }
    }
}
