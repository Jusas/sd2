using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SD2API.Application.Infrastructure;
using SD2API.Application.Search;
using SD2API.Domain;

namespace SD2API.Application.Core.Replays.Queries
{
    public class SimpleReplayModel : IMappedModel
    {
        public class Player : IMappedModel
        {
            public string PlayerUserId { get; set; }
            public double PlayerElo { get; set; }
            public long PlayerRank { get; set; }
            public long PlayerLevel { get; set; }
            public string PlayerName { get; set; }
            public string PlayerIALevel { get; set; }
            public string PlayerDeckContent { get; set; }
            public long PlayerAlliance { get; set; }

            public void SetMappings(Profile mappings)
            {
                mappings.CreateMap<ReplayHeader.ReplayHeaderPlayer, SimpleReplayModel.Player>();
            }
        }

        public string Hash { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string BinaryUrl { get; set; }

        public List<SimpleReplayModel.Player> Players { get; set; }

        public string Version { get; set; }
        public string GameMode { get; set; }
        public string Map { get; set; }
        public long NbMaxPlayer { get; set; }
        public long NbIA { get; set; }
        public string GameType { get; set; }
        public long InitMoney { get; set; }
        public long TimeLimit { get; set; }
        public long ScoreLimit { get; set; }
        public string VictoryCond { get; set; }
        public long IncomeRate { get; set; }
        public string ResultVictory { get; set; }
        public long ResultScore { get; set; }
        public long ResultDuration { get; set; }

        public virtual void SetMappings(Profile mappings)
        {
            mappings.CreateMap<Replay, SimpleReplayModel>()
                .ForMember(d => d.Hash, opt => opt.MapFrom(s => s.ReplayHashStub))
                .ForMember(d => d.Players, opt => opt.MapFrom(s => s.ReplayHeader.Players))
                .ForMember(d => d.GameMode, opt => opt.MapFrom(s => s.ReplayHeader.Game.GameMode))
                .ForMember(d => d.Version, opt => opt.MapFrom(s => s.ReplayHeader.Game.Version))
                .ForMember(d => d.Map, opt => opt.MapFrom(s => s.ReplayHeader.Game.Map))
                .ForMember(d => d.NbMaxPlayer, opt => opt.MapFrom(s => s.ReplayHeader.Game.NbMaxPlayer))
                .ForMember(d => d.NbIA, opt => opt.MapFrom(s => s.ReplayHeader.Game.NbIA))
                .ForMember(d => d.GameType, opt => opt.MapFrom(s => s.ReplayHeader.Game.GameType))
                .ForMember(d => d.InitMoney, opt => opt.MapFrom(s => s.ReplayHeader.Game.InitMoney))
                .ForMember(d => d.TimeLimit, opt => opt.MapFrom(s => s.ReplayHeader.Game.TimeLimit))
                .ForMember(d => d.ScoreLimit, opt => opt.MapFrom(s => s.ReplayHeader.Game.ScoreLimit))
                .ForMember(d => d.VictoryCond, opt => opt.MapFrom(s => s.ReplayHeader.Game.VictoryCond))
                .ForMember(d => d.IncomeRate, opt => opt.MapFrom(s => s.ReplayHeader.Game.IncomeRate))
                .ForMember(d => d.ResultDuration, opt => opt.MapFrom(s => s.ReplayFooter.result.Duration))
                .ForMember(d => d.ResultScore, opt => opt.MapFrom(s => s.ReplayFooter.result.Score))
                .ForMember(d => d.ResultVictory, opt => opt.MapFrom(s => s.ReplayFooter.result.Victory))
                ;
        }
    }
}
