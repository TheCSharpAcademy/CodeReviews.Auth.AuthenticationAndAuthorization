using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCSongs.Data;
using MVCSongs.Models;

namespace MVCSongs.Controllers
{
    public class SongsController : Controller
    {
        private readonly MVCSongsContext _context;
        private readonly ILogger<SongsController> _logger;

        public SongsController(MVCSongsContext context, ILogger<SongsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Songs
        [Authorize]
        public async Task<IActionResult> Index(string songGenre, string searchQuery)
        {
            if (_context.Song == null)
            {
                _logger.LogWarning($"Entity set 'MvcSongContext.Song' is null.");
                return Problem("Entity set 'MvcSongContext.Song' is null.");
            }

            IQueryable<string> genresQuery = _context.Song.OrderBy(s => s.Genre).Select(s => s.Genre);

            var songs = _context.Song.ToList();

            if (!String.IsNullOrEmpty(searchQuery)) {
                songs = songs.Where(s => s.Name!.ToUpper().Contains(searchQuery.ToUpper()) || s.Author!.ToUpper().Contains(searchQuery.ToUpper())).ToList();
            }

            if (!String.IsNullOrEmpty(songGenre))
            {
                songs = songs.Where(s => s.Genre == songGenre).ToList();
            }

            var songGenreViewModel = new SongGenreViewModel
            {
                Genres = new SelectList(await genresQuery.Distinct().ToListAsync()),
                Songs = songs
            };

            return View(songGenreViewModel);
        }

        // GET: Songs/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning($"Empty id was submitted to the details.");
                return NotFound();
            }

            var song = await _context.Song
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                _logger.LogWarning($"Tried to find a record #{id}. Could not find it.");
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Author,ReleaseDate,Genre,Price,TimesListened")] Song song)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Adding a song with the name of {song.Name}");
                _context.Add(song);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Song has been added.");
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Songs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning($"Tried to edit a record, but did not set the id. Could not find it.");
                return NotFound();
            }

            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                _logger.LogWarning($"Tried to edit record #{id}. Could not find it.");
                return NotFound();
            }
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Author,ReleaseDate,Genre,Price,TimesListened")] Song song)
        {
            if (id != song.Id)
            {
                _logger.LogWarning($"Tried to edit record #{id}. Could not find it.");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    _logger.LogInformation($"Record #{id} has been edited.");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
                    {
                        _logger.LogWarning($"Tried to edit record #{id}. Could not find it.");
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogWarning($"Tried to edit record #{id}. Error was thrown.");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Songs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning($"Tried to delete a record without an id. Could not find it.");
                return NotFound();
            }

            var song = await _context.Song
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                _logger.LogWarning($"Tried to delete record #{id}. Could not find it.");
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Song.FindAsync(id);
            if (song != null)
            {
                _logger.LogWarning($"Deleting a record with #{id}.");
                _context.Song.Remove(song);
            }

            _logger.LogInformation($"Record #{id} has been removed.");
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        private bool SongExists(int id)
        {
            return _context.Song.Any(e => e.Id == id);
        }
    }
}
