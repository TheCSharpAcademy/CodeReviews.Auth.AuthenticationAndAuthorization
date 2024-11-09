using Microsoft.EntityFrameworkCore;
using MVC_tutorial.Models;

namespace MVC_tutorial.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext (DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; } = default!;
    }
}
