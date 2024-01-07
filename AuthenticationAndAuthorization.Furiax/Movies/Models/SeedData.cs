using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using Movies.Data;

namespace Movies.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new MoviesContext(serviceProvider.GetRequiredService<DbContextOptions<MoviesContext>>())) 
            {
                if (context.Movie.Any())
                {
                    return;
                }
                context.Movie.AddRange
                    (new Movie
                    {
                        Title = "The Nun II",
                        ReleaseDate = DateTime.Parse("2023-09-13"),
                        Genre = "Horror",
                        Price = 21.99M,
                        Rating = "R"
                    },
                    new Movie{
                    Title = "The Equilizer 3",
                        ReleaseDate = DateTime.Parse("2023-09-1"),
                        Genre = "Action",
                        Price = 24.99M,
                        Rating = "R"
                    },
                    new Movie
                    {
                        Title = "Mission Impossible - Dead Reckoning Part One",
                        ReleaseDate = DateTime.Parse("2023-07-12"),
                        Genre = "Action",
                        Price = 19.99M,
                        Rating = "PG-13"
                    },
                    new Movie
                    {
                        Title = "Gran Turismo",
                        ReleaseDate = DateTime.Parse("2023-08-11"),
                        Genre = "Action",
                        Price = 20.99M,
                        Rating = "PG-13"
                    },
                    new Movie
                    {
                        Title = "Barbie",
                        ReleaseDate = DateTime.Parse("2023-07-21"),
                        Genre = "Comedy",
                        Price = 19.99M,
                        Rating = "PG-13"
                    },
                    new Movie
                    {
                        Title = "Indiana Jones and the Dial of Destiny",
                        ReleaseDate = DateTime.Parse("2023-06-30"),
                        Genre = "Adventure",
                        Price = 21.99M
                        ,
                        Rating = "PG-13"
                    },
                    new Movie
                    {
                        Title = "No One Will Save You",
                        ReleaseDate = DateTime.Parse("2023-09-22"),
                        Genre = "Horror",
                        Price = 14.99M,
                        Rating = "PG-13"
                    });
                context.SaveChanges();
            }

            using (var context = new MoviesContext(serviceProvider.GetRequiredService<DbContextOptions<MoviesContext>>()))
            {
                if (context.Music.Any())
                {
                    return;
                }
                context.Music.AddRange
                    (new Music
                    {
                        Artist = "Maneskin",
                        AlbumName = "Rush!",
                        ReleaseDate = DateTime.Parse("2023-01-20"),
                        Genre = "Pop Rock",
                        Price = 17.50M
                    }, 
                    new Music
                    {
                        Artist = "Korn",
                        AlbumName = "Requiem Mass",
                        ReleaseDate = DateTime.Parse("2023-02-03"),
                        Genre = "Metal",
                        Price = 18.00M
                    },
                    new Music
                    {
                        Artist = "Pink",
                        AlbumName = "Trustfall",
                        ReleaseDate = DateTime.Parse("2023-02-17"),
                        Genre = "Pop",
                        Price = 16.99M
                    },
                    new Music
                    {
                        Artist = "Macklemore",
                        AlbumName = "Ben",
                        ReleaseDate = DateTime.Parse("2023-03-03"),
                        Genre = "Hip hop",
                        Price = 15.50M
                    },
                    new Music
                    {
                        Artist = "Fall Out Boy",
                        AlbumName = "So Much (for) Stardust",
                        ReleaseDate = DateTime.Parse("2023-03-24"),
                        Genre = "Pop Rock",
                        Price = 17.50M
                    },
                    new Music
                    {
                        Artist = "Metallica",
                        AlbumName = "72 Seasons",
                        ReleaseDate = DateTime.Parse("2023-04-14"),
                        Genre = "Metal",
                        Price = 19.99M
                    },
                    new Music
                    {
                        Artist = "Tiësto",
                        AlbumName = "Drive",
                        ReleaseDate = DateTime.Parse("2023-04-21"),
                        Genre = "Dance",
                        Price = 18.00M
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
