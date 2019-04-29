using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SD2API.Domain;

namespace SD2API.Application.Interfaces
{
    public interface IApiDbContext
    {
        DbSet<Replay> Replays { get; set; }
        DbSet<ReplayHeader.ReplayHeaderGame> ReplayHeaderGame { get; set; }
        DbSet<ReplayHeader.ReplayHeaderPlayer> ReplayHeaderPlayer { get; set; }

        // Originates from DbContext.
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
