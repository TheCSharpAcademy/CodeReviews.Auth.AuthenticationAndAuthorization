using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Movies.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Movies.Data;

public class MovieContext(DbContextOptions<MovieContext> options) : IdentityDbContext<IdentityUser, IdentityRole, string>(options)
{
    public DbSet<Movie> Movie { get; set; } = default!;
    public DbSet<TvShow> TvShow { get; set; } = default!;
}

