using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SD2API.Application.Core.Replays.Queries;
using SD2API.Application.Infrastructure;
using SD2API.Application.Interfaces;
using SD2API.Application.Search;
using SD2API.Persistence;
using SD2API.Startup;
using SD2API.Startup.Configuration;
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
            var config = new ConfigurationBuilder()
                .AddMemCachedKeyVaultConfiguration(() => Environment.GetEnvironmentVariable("AzureKeyVaultUri"))
                .AddEnvironmentVariables("SD2API_")
                .Build();

            services.AddSingleton(typeof(IConfiguration), config);
            services.AddTransient<IQueryBuilder, QueryBuilder>();
            services.AddMediatR(typeof(GetReplayResponse).GetTypeInfo().Assembly);
            services.AddSingleton<IMapper, Mapper>(provider =>
                new Mapper(
                    new MapperConfiguration(
                        cfg =>
                        {
                            cfg.AddProfile(new AutoMapperProfile());
                            cfg.CreateMap<double, string>().ConvertUsing(x => x.ToString(CultureInfo.InvariantCulture));
                            cfg.CreateMap<string, double>().ConvertUsing(x => double.Parse(x, CultureInfo.InvariantCulture));
                        })
                )
            );

            services.AddDbContext<IApiDbContext, ApiDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("Db"));
            });
            services.AddTransient<IBlobStorage, AzureBlobStorage>();
            // problem: output goes to bin/bin, and dotnet ef core commands can't find it.
            var dbContextInstance = services.BuildServiceProvider().GetService<IApiDbContext>() as ApiDbContext;
            dbContextInstance.Database.Migrate();

        }
    }
}
