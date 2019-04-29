using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SD2API.Application.Interfaces;

namespace SD2API.Application.Core.Maps.Queries
{
    public class GetMapListHandler : IRequestHandler<GetMapList, GetMapListResponse>
    {
        private IApiDbContext _apiDbContext;

        public GetMapListHandler(IApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<GetMapListResponse> Handle(GetMapList request, CancellationToken cancellationToken)
        {
            var maps = await _apiDbContext.ReplayHeaderGame.Select(x => x.Map).Distinct().ToListAsync();
            var response = new GetMapListResponse();
            response.AddRange(maps);
            return response;
        }
    }
}
