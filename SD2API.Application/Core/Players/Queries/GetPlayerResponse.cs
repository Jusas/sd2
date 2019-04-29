using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SD2API.Application.Infrastructure;
using SD2API.Domain;

namespace SD2API.Application.Core.Players.Queries
{
    public class GetPlayerResponse : IMappedModel
    {
        public GetPlayerResponse()
        {
            KnownPlayerNames = new List<string>();
        }
        public string PlayerUserId { get; set; }
        public double PlayerElo { get; set; }
        public int PlayerRank { get; set; }
        public int PlayerLevel { get; set; }
        public List<string> KnownPlayerNames { get; set; }

        public void SetMappings(Profile mappings)
        {
            mappings.CreateMap<ReplayHeader.ReplayHeaderPlayer, GetPlayerResponse>();

        }
    }
}
