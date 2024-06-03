using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers;

[Authorize]
public class TvShows(MovieContext context) : Controller
{
    private readonly MovieContext _context = context;

    public async Task<IActionResult> Index(string tvShowGenre, string searchString)
    {
        if(_context.TvShow is null)
            return Problem("Entity set 'MvcMovieContext.Movie'  is null.");

        var shows = _context.TvShow.AsQueryable();
        var genres = _context.TvShow.Select(p => p.Genre).Distinct();

        if(searchString is not null)
            shows = shows.Where(p => p.Title!.Contains(searchString));

        if(tvShowGenre is not null)
            shows = shows.Where( p => p.Genre == tvShowGenre);

        var showsViewModel = new TvShowViewModel
            {
                TvShows = await shows.ToListAsync(),
                Genres = new SelectList(await genres.ToListAsync()),
            };
        

        return View(showsViewModel);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if(id is null)
            return RedirectToAction(nameof(Index));

        var show = await _context.TvShow.FindAsync( id);

        if(show is null)
            return NotFound();
        return View(show);        
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if(id is null)
            return RedirectToAction(nameof(Index));

        var show = await _context.TvShow.FindAsync( id);

        if(show is null)
            return NotFound();
        return View(show);
    }

    [HttpPost, ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] TvShow show)
    {
        if(!ModelState.IsValid)
            return View(show);

        if(!id.Equals(show.Id))
            return NotFound();

        _context.TvShow.Update(show);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch(DBConcurrencyException)
        {
            if (!_context.TvShow.Any(p => p.Id.Equals(id)))
                return NotFound();
            else
                throw;
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if( id is null)
            return NotFound();
        
        var show = await _context.TvShow.FindAsync(id);

        if( show is null )
            return NotFound();

        return View(show);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePost(int? id)
    {
        if( id is null)
            return NotFound();
        
        var show = await _context.TvShow.FindAsync(id);

        if( show is null )
            return NotFound();

        _context.TvShow.Remove(show);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create( [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] TvShow show)
    {
        if( !ModelState.IsValid )
            return View(show);
        
        _context.TvShow.Add(show);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch(DBConcurrencyException)
        {
            throw;
        }

        return RedirectToAction(nameof(Index));
    }
}
