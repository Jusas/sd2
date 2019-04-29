using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using AutoMapper;
using AzureFunctionsV2.HttpExtensions.Authorization;
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

        private static IConfiguration _configuration;
        private static IConfiguration Configuration
        {
            get
            {
                if (_configuration != null)
                    return _configuration;

                _configuration = new ConfigurationBuilder()
                    .AddMemCachedKeyVaultConfiguration(() => Environment.GetEnvironmentVariable("AzureKeyVaultUri"))
                    .AddEnvironmentVariables("SD2API_")
                    .Build();
                return _configuration;
            }
        }

        public void Configure(IWebJobsBuilder builder)
        {
            // Basic auth
            builder.Services.Configure<HttpAuthenticationOptions>(opts =>
            {
                var userPass = Configuration.GetSection("Auth")["AdminCreds"].Split(":");
                opts.BasicAuthentication = new BasicAuthenticationParameters()
                {
                    ValidCredentials = new Dictionary<string, string>() { { userPass[0], userPass[1] } }
                };
            });

            builder.AddDependencyInjection(ConfigureServices);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IConfiguration), Configuration);
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
                options.UseSqlServer(Configuration.GetConnectionString("Db"));
            });
            services.AddTransient<IBlobStorage, AzureBlobStorage>();

            var dbContextInstance = services.BuildServiceProvider().GetService<IApiDbContext>() as ApiDbContext;
            dbContextInstance.Database.Migrate();

        }
    }
}
