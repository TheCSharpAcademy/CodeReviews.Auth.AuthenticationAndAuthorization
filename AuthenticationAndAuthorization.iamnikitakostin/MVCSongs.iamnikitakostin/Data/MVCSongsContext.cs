using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCSongs.Models;

namespace MVCSongs.Data
{
    public class MVCSongsContext : IdentityDbContext
    {
        public MVCSongsContext (DbContextOptions<MVCSongsContext> options)
            : base(options)
        {
        }

        public DbSet<MVCSongs.Models.Song> Song { get; set; } = default!;
        public DbSet<Error> Errors { get; set; } = default!;
    }
}
