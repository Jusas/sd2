using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SD2API.Application.Search;

namespace SD2API.Application.Core.Replays.Queries
{
    public class GetReplayList : IRequest<GetReplayListModel>
    {
        public int Skip { get; set; }
        public int Limit { get; set; }
        public List<SearchCriterion> SearchCriteria { get; set; }
        public string OrderingField { get; set; }
        public bool Descending { get; set; }
    }
}
