using Microsoft.EntityFrameworkCore;
using MVCSongs.Data;

namespace MVCSongs.Models;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MVCSongsContext(
          serviceProvider.GetRequiredService<
              DbContextOptions<MVCSongsContext>>()))
        {
            // Look for any movies.
            if (context.Song.Any())
            {
                return;   // DB has been seeded
            }
            context.Song.AddRange(
                new Song
                {
                    Name = "Sally-Sally",
                    Author = "Lonely Band",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Genre = "Alternative Rock",
                    Price = 9.99M,
                    TimesListened = 768932
                },
                new Song
                {
                    Name = "Taxi",
                    Author = "Unknown",
                    ReleaseDate = DateTime.Parse("2020-1-02"),
                    Genre = "Dubstep",
                    Price = 0.99M,
                    TimesListened = 337
                },
                new Song
                {
                    Name = "Nights",
                    Author = "Avicii",
                    ReleaseDate = DateTime.Parse("2009-2-09"),
                    Genre = "Pop",
                    Price = 1.99M,
                    TimesListened = 584893810
                }
            );
            context.SaveChanges();
        }
    }
}
