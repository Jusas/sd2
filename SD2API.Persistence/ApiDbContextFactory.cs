using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SD2API.Persistence
{
    /// <summary>
    /// Merely for migrations.
    /// set ApiDbConnectionString=Server=tcp:localhost,1433;Initial Catalog=sd2replays;Persist Security Info=False;User ID=sd2replays;Password=msx3oy68PLGBVJ6NZPT3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;
    /// dotnet ef migrations add InitialMigration --context ApiDbContext --project SD2API.Persistence.csproj -o Migrations
    /// </summary>
    public class ApiDbContextFactory : IDesignTimeDbContextFactory<ApiDbContext>
    {
        public ApiDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApiDbContext>();
            var connectionString = Environment.GetEnvironmentVariable("ApiDbConnectionString");
            builder.UseSqlServer(connectionString);
            return new ApiDbContext(builder.Options);
        }
    }
}
