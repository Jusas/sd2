using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SD2API.Application.Interfaces;
using SD2API.Domain;

namespace SD2API.Persistence
{
    public class ApiDbContext : DbContext, IApiDbContext
    {

        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Replay> Replays { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
        }
    }
}
