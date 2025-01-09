using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookMvc.Data;
using BookMvc.Models;
using Microsoft.AspNetCore.Authorization;


namespace BookMvc.Controllers
{
    public class BookController : Controller
    {
        private readonly BookContext _context;

        public BookController(BookContext context)
        {
            _context = context;
        }

        // GET: BookMvc
        public async Task<IActionResult> Index(BookSearchOptions? searchOptions = null)
        {
            IEnumerable<Book> books =  await _context.Book.ToListAsync();
            if (searchOptions != null)
            {
                if(searchOptions.Genre != Genre.None)
                    books = books.Where(b => b.Genre == searchOptions.Genre);
                if(searchOptions.Author != null)
                    books = books.Where(b => b.Author.ToLower().Contains( searchOptions.Author.ToLower()));
                if(searchOptions.Title != null)
                    books = books.Where(b => b.Title.ToLower().Contains(searchOptions.Title.ToLower()));
                if(searchOptions.Year != -1)
                    books = books.Where(b => b.Year == searchOptions.Year);
                if(searchOptions.Price != -1)
                    books = books.Where(b => b.Price <= searchOptions.Price);
                if(searchOptions.Rating != -1)
                    books = books.Where(b => b.Rating >= searchOptions.Rating);
                if(searchOptions.ReadingStatus != ReadingStatus.None)
                    books = books.Where(b => b.ReadingStatus == searchOptions.ReadingStatus);
            }
            return View(books);
        }

        // GET: BookMvc/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: BookMvc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookMvc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Year,Description,CoverImageUrl,ReadingStatus,Genre,Rating,Price")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: BookMvc/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: BookMvc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Year,Description,CoverImageUrl,ReadingStatus,Genre,Rating,Price")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: BookMvc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: BookMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
