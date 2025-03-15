using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCSongs.Models;

public class SongGenreViewModel
{
    public List<Song>? Songs { get; set; }
    public SelectList? Genres { get; set; }
    public string? SongGenre {  get; set; }
    public string? SearchQuery { get; set; }
}
