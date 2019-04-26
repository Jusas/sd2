using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SD2API.Application.Core.Replays.Exceptions;
using SD2API.Application.Infrastructure.Expressions;
using SD2API.Application.Interfaces;
using SD2API.Application.Search;
using SD2API.Domain;
using StringComparison = SD2API.Application.Search.StringComparison;

namespace SD2API.Application.Core.Replays.Queries
{
    class GetReplayListHandler : IRequestHandler<GetReplayList, GetReplayListResponse>
    {
        private IApiDbContext _dbContext;
        private IMapper _mapper;
        private IQueryBuilder _queryBuilder;

        public GetReplayListHandler(IApiDbContext dbContext, IMapper mapper, IQueryBuilder queryBuilder)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _queryBuilder = queryBuilder;
        }

        public async Task<GetReplayListResponse> Handle(GetReplayList request, CancellationToken cancellationToken)
        {
            Expression<Func<Replay, bool>> finalWhereExpression = x => true;

            if (request.Query != null)
            {
                var queryExpressions = new List<Expression<Func<Replay, bool>>>();
                if (!string.IsNullOrEmpty(request.Query.Name))
                {
                    Expression<Func<Replay, bool>> ex = x => x.Name.Contains(request.Query.Name);
                    queryExpressions.Add(ex);
                }
                if (!string.IsNullOrEmpty(request.Query.GameMode))
                {
                    Expression<Func<Replay, bool>> ex = x => x.ReplayHeader.Game.GameMode == request.Query.GameMode;
                    queryExpressions.Add(ex);
                }
                if (!string.IsNullOrEmpty(request.Query.Map))
                {
                    Expression<Func<Replay, bool>> ex = x => x.ReplayHeader.Game.Map == request.Query.Map;
                    queryExpressions.Add(ex);
                }
                if (!string.IsNullOrEmpty(request.Query.PlayerId))
                {
                    Expression<Func<Replay, bool>> ex = x => x.ReplayHeader.Players.Any(p => p.PlayerUserId == request.Query.PlayerId);
                    queryExpressions.Add(ex);
                }
                if (!string.IsNullOrEmpty(request.Query.PlayerName))
                {
                    Expression<Func<Replay, bool>> ex = x => x.ReplayHeader.Players.Any(p => p.PlayerName.ToLowerInvariant().Contains(request.Query.PlayerName.ToLowerInvariant()));
                    queryExpressions.Add(ex);
                }
                if (!string.IsNullOrEmpty(request.Query.VictoryCond))
                {
                    Expression<Func<Replay, bool>> ex = x => x.ReplayHeader.Game.VictoryCond == request.Query.VictoryCond;
                    queryExpressions.Add(ex);
                }
                if (request.Query.IncomeRate != null)
                {
                    Expression<Func<Replay, bool>> ex = x => x.ReplayHeader.Game.IncomeRate == request.Query.IncomeRate.ToString();
                    queryExpressions.Add(ex);
                }
                if (request.Query.InitMoney != null)
                {
                    Expression<Func<Replay, bool>> ex = x => x.ReplayHeader.Game.InitMoney == request.Query.InitMoney.ToString();
                    queryExpressions.Add(ex);
                }
                if (request.Query.NbMaxPlayer != null)
                {
                    Expression<Func<Replay, bool>> ex = x => x.ReplayHeader.Game.NbMaxPlayer == request.Query.NbMaxPlayer.ToString();
                    queryExpressions.Add(ex);
                }
                if (request.Query.RankedMatchesOnly != null && request.Query.RankedMatchesOnly == true)
                {
                    Expression<Func<Replay, bool>> ex = x => x.ReplayHeader.Game.GameType == "1";
                    queryExpressions.Add(ex);
                }

                if (queryExpressions.Count == 1)
                    finalWhereExpression = queryExpressions.First();
                else if(queryExpressions.Count > 0)
                {
                    var combined = queryExpressions.First();
                    for (var i = 1; i < queryExpressions.Count; i++)
                    {
                        combined = combined.AndAlso(queryExpressions[i]);
                    }

                    finalWhereExpression = combined;
                }
            }

            var limit = request.Limit == 0 ? 25 : Math.Min(25, request.Limit);
            var skip = request.Skip;

            var count = await _dbContext.Replays.CountAsync(finalWhereExpression);
            var matches = await _dbContext.Replays
                .Include(r => r.ReplayHeader)
                .ThenInclude(h => h.Players)
                .Include(r => r.ReplayHeader)
                .ThenInclude(h => h.Game)
                .Include(r => r.ReplayFooter)
                .ThenInclude(f => f.result)
                .Where(finalWhereExpression).OrderBy(x => x.Date).Skip(skip).Take(limit).ToListAsync();

            var result = new GetReplayListResponse()
            {
                Limit = limit,
                Skip = skip,
                Total = count,
                Replays = matches.Select(x => _mapper.Map<SimpleReplayModel>(x)).ToList()
            };
            return result;
        }


    }
}
