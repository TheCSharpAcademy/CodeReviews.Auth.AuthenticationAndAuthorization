using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Models
{
    public class Music
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3), Required]
        public string? Artist { get; set; }
        [StringLength(60, MinimumLength = 3), Display(Name ="Album Name"), Required]
        public string? AlbumName { get; set; }
        [Display(Name = "Release Date"), DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"),Required, StringLength(30)]   
        public string? Genre { get; set; }
        [Column(TypeName = "decimal(18,2)"), DataType(DataType.Currency), Range(1, 50)]
        public decimal Price { get; set; }
    }
}
