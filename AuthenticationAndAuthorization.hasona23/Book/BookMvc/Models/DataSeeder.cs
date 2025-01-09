using BookMvc.Data;
using Microsoft.EntityFrameworkCore;

namespace BookMvc.Models;

public class DataSeeder
{
    private static ReadingStatus GetRandomReadingStatus()
    {
        return Enum.GetValues(typeof(ReadingStatus)).Cast<ReadingStatus>().Where(e => e != ReadingStatus.None)
            .ElementAt(new Random().Next(0, Enum.GetValues(typeof(ReadingStatus)).Length - 1));
    }

    private static Genre GetRandomGenre()
    {
        return Enum.GetValues(typeof(Genre)).Cast<Genre>().Where(e => e != Genre.None).ElementAt(new Random().Next(0, Enum.GetValues(typeof(Genre)).Length - 1));
    }

    private static string[] _coversUrl = ["https://www.designforwriters.com/wp-content/uploads/2017/10/design-for-writers-book-cover-tf-2-a-million-to-one.jpg"
    ,"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRZ1K_Y8KVyiu2GbBwKQix-8Bi7yhDsxOQBUw&s",
    "https://images.squarespace-cdn.com/content/v1/5fc7868e04dc9f2855c99940/d13d2ad3-bca7-4544-b7bf-71bf7af7b283/laura-barrett-illustration-moon-stars-book-cover-design.jpg",
    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSWIvmFNeQOpKasi-_Bjqo-_mC_OQvrtUiT5g&s"];

    private static string GetRandomCoverUrl()
    {
        return _coversUrl[new Random().Next(0, _coversUrl.Length)];
    }
    public static void SeedBooksData(IServiceProvider serviceProvider,bool reseed = false)
    {
           
        using BookContext context = new BookContext(serviceProvider.GetService<DbContextOptions<BookContext>>()!);
        if (reseed)
        {
            context.Book.RemoveRange(context.Book);
            context.SaveChanges();
        }
        if (context.Book.Any())
        {
            return;
        }

        for (int i = 0; i < 100; i++)
        {
            context.Add(
            new Book
            {
                Title = $"Book {i}",
                Author = $"Author {i}",
                Year = new Random().Next(1800,2100),
                CoverImageUrl = GetRandomCoverUrl(),
                Description = $"description {i}description {i}",
                ReadingStatus = GetRandomReadingStatus(),
                Genre = GetRandomGenre(),
                Rating = new Random().Next(0, 6),
                Price = new Random().Next(0, 1000),
                
            });
        }
        context.SaveChanges();
    }
}