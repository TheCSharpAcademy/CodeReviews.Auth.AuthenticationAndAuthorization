namespace BookMvc.Models;

public class BookSearchOptions
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public int Year { get; set; } = -1;
    public ReadingStatus ReadingStatus { get; set; } = ReadingStatus.None;
    public Genre Genre { get; set; } = Genre.None;
    public float Rating { get; set; } = -1;
    public float Price { get; set; } = -1;
}