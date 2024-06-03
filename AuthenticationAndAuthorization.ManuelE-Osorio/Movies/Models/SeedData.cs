using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using System.Globalization;

namespace Movies.Models;

public static class SeedData
{
    public static void InitializeMovies(IServiceProvider serviceProvider)
    {
        using var context = new MovieContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MovieContext>>());


        if (context.Movie.Any())
        {
            return;
        }
        context.Movie.AddRange(
            new Movie
            {
                Title = "Citizen Kane",
                ReleaseDate = DateTime.ParseExact("1989-09-05", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Genre = "Drama",
                Rating = "PG",
                Price = 7.99M
            },
            new Movie
            {
                Title = "The Good, the Bad and the Ugly",
                ReleaseDate = DateTime.ParseExact("1966-12-23", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Genre = "Western",
                Price = 8.99M
            },
            new Movie
            {
                Title = "The Goonies",
                ReleaseDate = DateTime.ParseExact("1985-06-07", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Genre = "Comedy",
                Rating = "PG",
                Price = 9.99M
            },
            new Movie
            {
                Title = "Chinatown",
                ReleaseDate = DateTime.ParseExact("1974-06-20", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Genre = "Thriller",
                Rating = "R",
                Price = 3.99M
            },
            new Movie
            {
                Title = "The Godfather Part II",
                ReleaseDate = DateTime.ParseExact("1974-12-12", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Genre = "Drama",
                Rating = "R",
                Price = 25.99M
            },
            new Movie
            {
                Title = "One Flew Over the Cuckoo's Nest",
                ReleaseDate = DateTime.ParseExact("1975-11-19", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Genre = "Drama",
                Rating = "R",
                Price = 17M
            }
        );
        context.SaveChanges();
    }

    public static void InitializeTvShows(IServiceProvider services)
    {
        using var context = new MovieContext(
            services.GetRequiredService<
                DbContextOptions<MovieContext>>());
        if (context.TvShow.Any())
        {
            return;
        }
        context.TvShow.AddRange(
            new TvShow
            {
                Title = "The Sopranos",
                ReleaseDate = DateTime.ParseExact("1999-01-10", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Genre = "Crime",
                Rating = "TV-MA",
                Price = 7.99M
            },
            new TvShow
            {
                Title = "Breaking Bad",
                ReleaseDate = DateTime.ParseExact("2008-01-20", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Genre = "Crime",
                Rating = "TV-MA",
                Price = 8.99M
            },
            new TvShow
            {
                Title = "Seinfeld",
                ReleaseDate = DateTime.ParseExact("1989-07-05", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Genre = "Comedy",
                Rating = "TV-PG",
                Price = 12.99M
            },
            new TvShow
            {
                Title = "House",
                ReleaseDate = DateTime.ParseExact("2004-11-16", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Genre = "Drama",
                Rating = "TV-14",
                Price = 15.99M
            }
        );
        context.SaveChanges();
    }

    public static async Task<bool> InitializeUser(IServiceProvider services)
    {
        using var context = new MovieContext(
            services.GetRequiredService<DbContextOptions<MovieContext>>());
        context.Database.EnsureCreated();

        if(context.Users.Any())
        {
            return false;
        }
        
        using var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        using var _userStore = services.GetRequiredService<IUserStore<IdentityUser>>();
        using var _emailStore = (IUserEmailStore<IdentityUser>)_userStore;
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await roleManager.CreateAsync(new IdentityRole("Admin"));
        

        var user = Activator.CreateInstance<IdentityUser>();
        await _userStore.SetUserNameAsync(user, "admin@thecsharpacademy.com", CancellationToken.None);
        await _emailStore.SetEmailAsync(user, "admin@thecsharpacademy.com", CancellationToken.None);
        await userManager.CreateAsync(user, "Admin1234)(/2508");
        await userManager.AddToRoleAsync(user, "Admin");
        
        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
        await userManager.ConfirmEmailAsync(user, code);
        return true;
    }
}
