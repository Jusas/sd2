using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SD2Tools.ReplayTools.Models;
using Replay = SD2Tools.ReplayTools.Replay;
using ReplayHeader = SD2API.Domain.ReplayHeader;

namespace SD2API.Application.Infrastructure
{
    public class ReplayToolsMappings : IMappedModel
    {
        public void SetMappings(Profile mappings)
        {
            mappings.CreateMap<Game, ReplayHeader.ReplayHeaderGame>();
            mappings.CreateMap<Player, ReplayHeader.ReplayHeaderPlayer>();
            mappings.CreateMap<SD2Tools.ReplayTools.Models.ReplayHeader, ReplayHeader>();

            mappings.CreateMap<ReplayFooter.ReplayFooterResult, Domain.ReplayFooter.ReplayFooterResult>();
            mappings.CreateMap<ReplayFooter, Domain.ReplayFooter>()
                .ForMember(x => x.result, opts => opts.MapFrom(x => x.Result));
        }
    }
}
