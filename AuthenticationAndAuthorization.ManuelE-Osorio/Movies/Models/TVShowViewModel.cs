using Microsoft.AspNetCore.Mvc.Rendering;

namespace Movies.Models;

public class TvShowViewModel
{
    public List<TvShow> TvShows { get; set; } = [];
    public SelectList? Genres { get; set; }
    public string? TvShowGenre { get; set; }
    public string? SearchString { get; set; }
}
