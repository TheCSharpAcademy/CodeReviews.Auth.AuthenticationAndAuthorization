using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieMvc.Models;


namespace MovieMvc.Data
{
    public class MovieMvcContext : IdentityDbContext
    {
        public MovieMvcContext (DbContextOptions<MovieMvcContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;
        public DbSet<Logs> Logs { get; set; } = default!;
    }
}
