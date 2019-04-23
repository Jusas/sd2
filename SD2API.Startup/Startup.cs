using System;
using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SD2API.Application.Core.Replays.Queries;
using SD2API.Application.Interfaces;
using SD2API.Persistence;
using SD2API.Startup;
using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(Startup), "MyStartup")]

namespace SD2API.Startup
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddDependencyInjection(ConfigureServices);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(GetReplayModel).GetTypeInfo().Assembly);
            services.AddDbContext<IApiDbContext, ApiDbContext>(options =>
            {
                options.UseSqlServer(StartupConfiguration.DatabaseConnectionString);
            });
            // problem: output goes to bin/bin, and dotnet ef core commands can't find it.
            var dbContextInstance = services.BuildServiceProvider().GetService<IApiDbContext>() as ApiDbContext;
            //dbContextInstance.Database.Migrate();

        }
    }
}
