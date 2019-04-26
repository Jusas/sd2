using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SD2API.Application.Infrastructure;
using SD2API.Domain;

namespace SD2API.Application.Core.Replays.Queries
{
    public class GetReplayResponse : SimpleReplayModel
    {
        public override void SetMappings(Profile mappings)
        {
            base.SetMappings(mappings);

            mappings.CreateMap<Replay, GetReplayResponse>()
                .IncludeBase<Replay, SimpleReplayModel>();
        }
    }
}
