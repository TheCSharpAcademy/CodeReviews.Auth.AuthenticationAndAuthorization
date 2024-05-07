using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models;

public class TvShow
{
    public int Id { get; set; }

    [StringLength(60, MinimumLength = 3)]
    public string? Title { get; set; }

    [Display(Name = "Release Date"), DataType(DataType.Date)]
    public DateTime ReleaseDate { get; set; }
    
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Only letters are allowed, it has to start with a capital letter."), 
    Required, StringLength(30)]
    public string? Genre { get; set; }
    
    [Range(1, int.MaxValue), DataType(DataType.Currency), Column(TypeName = "decimal(18, 2)")]
    [DisplayFormat(DataFormatString="{0:C}")]
    public decimal Price { get; set; }

    [StringLength(60, MinimumLength = 1)]
    public string? Rating { get; set; }
}