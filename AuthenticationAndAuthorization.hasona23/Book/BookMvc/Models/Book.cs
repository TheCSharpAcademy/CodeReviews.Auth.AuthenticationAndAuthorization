using System.ComponentModel.DataAnnotations;

namespace BookMvc.Models;

public class Book
{
    public int Id { get; set; }
    [Required]
    [StringLength(30,MinimumLength = 1)]
    public string? Title { get; set; }
    [Required]
    [StringLength(30,MinimumLength = 5)]
    public string? Author { get; set; }
    [Display(Name = "Year")]
    [Range(1800,2100)]
    public int Year { get; set; }
    [Required]
    [StringLength(320,MinimumLength = 20)]
    public string? Description { get; set; }
    [Url]
    [Required]
    [StringLength(200, MinimumLength = 10)]
    public string? CoverImageUrl { get; set; }
    [Required]
    public ReadingStatus ReadingStatus { get; set; }
    [Required]
    public Genre Genre { get; set; }
    [Required]
    [Range(0,5)]
    public float Rating { get; set; }
    [Required]
    [Range(0,1000)]
    [DataType(DataType.Currency)]
    public float Price { get; set; }

}

public enum ReadingStatus
{
    [Display(Name = "Didn't Read")]
    NotRead,
    Reading,
    Completed,
    None
}

public enum Genre
{
    Comedy,
    Drama,
    Fantasy,
    Horror,
    Romance,
    SciFi,
    Thriller,
    Fiction,
    [Display(Name = "Non Fiction")]
    NonFiction,
    Biography,
    Adventure,
    Tragedy,
    History,
    Other,
    Children,
    Fable,
    None
}