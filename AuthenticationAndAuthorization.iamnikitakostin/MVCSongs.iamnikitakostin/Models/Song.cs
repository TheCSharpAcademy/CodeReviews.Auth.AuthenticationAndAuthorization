using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCSongs.Models;

public class Song
{
    public int Id { get; set; }
    [StringLength(30, MinimumLength = 1)]
    [Required]
    public string Name { get; set; }
    [StringLength(30, MinimumLength = 3)]
    [Required]
    public string Author { get; set; }
    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleaseDate {  get; set; }
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    [Required]
    [StringLength(30)]
    public string? Genre { get; set; }
    [Range(0, 100)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    [DisplayFormat(DataFormatString = "{0:N0}")]
    [Display(Name = "Times Listened")]
    public int TimesListened { get; set; }
}
