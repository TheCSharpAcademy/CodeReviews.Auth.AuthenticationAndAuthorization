using Microsoft.AspNetCore.Mvc.Rendering;

namespace Movies.Models;

public class ValidRatings
{
    public static readonly List<SelectListItem> MovieRatings = new(
        [
        new() {Text = "G", Value = "G"},
        new() {Text = "PG", Value = "PG"},
        new() {Text = "PG-13", Value = "PG-13"},
        new() {Text = "R", Value = "R"},
        new() {Text = "NC-13", Value = "NC-17"},]);

    public static readonly List<SelectListItem> TvShowsRatings = new(
        [
        new() {Text = "TV-Y", Value = "TV-Y"},
        new() {Text = "TV-Y7", Value = "TV-Y7"},
        new() {Text = "TV-G", Value = "TV-G"},
        new() {Text = "TV-PG", Value = "TV-PG"},
        new() {Text = "TV-14", Value = "TV-14"},
        new() {Text = "TV-MA", Value = "TV-MA"},]);
}