using System;
using System.Collections.Generic;
using System.Text;

namespace SD2API.Application.Core.Replays.Queries
{
    public class GetReplayListResponse
    {
        public int Total { get; set; }
        public int Skip { get; set; }
        public int Limit { get; set; }
        public List<SimpleReplayModel> Replays { get; set; }

    }
}
