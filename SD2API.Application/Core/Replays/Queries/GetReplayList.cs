using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SD2API.Application.Search;

namespace SD2API.Application.Core.Replays.Queries
{
    public class GetReplayList : IRequest<GetReplayListResponse>
    {
        public int Skip { get; set; }
        public int Limit { get; set; }
        public ReplayQuery Query { get; set; }
        public string OrderBy { get; set; }
        public bool Descending { get; set; }

        public class ReplayQuery
        {
            public string Name { get; set; }
            public string PlayerName { get; set; }
            public string PlayerUserId { get; set; }
            public string Map { get; set; }
            public string GameMode { get; set; }
            public int? NbMaxPlayer { get; set; }
            public string VictoryCond { get; set; }
            public int? IncomeRate { get; set; }
            public int? InitMoney { get; set; }
            public bool? RankedMatchesOnly { get; set; }
        }
    }
}
