using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using SD2API.Application.Interfaces;
using SD2API.Domain;

namespace SD2API.Persistence
{
    public class ApiDbContext : DbContext, IApiDbContext
    {

        private static readonly LoggerFactory _loggerFactory = new LoggerFactory(new []{new DebugLoggerProvider()});

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Replay> Replays { get; set; }
        public DbSet<ReplayHeader.ReplayHeaderPlayer> ReplayHeaderPlayer { get; set; }
        public DbSet<ReplayHeader.ReplayHeaderGame> ReplayHeaderGame { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
        }
    }
}
