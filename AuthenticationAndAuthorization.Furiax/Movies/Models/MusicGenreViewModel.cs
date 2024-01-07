using Microsoft.AspNetCore.Mvc.Rendering;

namespace Movies.Models
{
    public class MusicGenreViewModel
    {
        public List<Music>? Music { get; set; }
        public SelectList? Genres { get; set; }
        public string? MusicGenre { get; set; }
        public string? ArtistSearch { get; set; }

    }
}
