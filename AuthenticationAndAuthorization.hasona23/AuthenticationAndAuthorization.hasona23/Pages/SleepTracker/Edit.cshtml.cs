using AuthenticationAndAuthorization.hasona23.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAndAuthorization.hasona23.Components.SleepTracker
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly AuthenticationAndAuthorization.hasona23.Data.SleepTrackerContext _context;

        public EditModel(AuthenticationAndAuthorization.hasona23.Data.SleepTrackerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SleepRecordModel SleepRecordModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sleeprecordmodel =  await _context.SleepRecords.FirstOrDefaultAsync(m => m.Id == id);
            if (sleeprecordmodel == null)
            {
                return NotFound();
            }
            SleepRecordModel = sleeprecordmodel;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SleepRecordModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SleepRecordModelExists(SleepRecordModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SleepRecordModelExists(int id)
        {
            return _context.SleepRecords.Any(e => e.Id == id);
        }
    }
}
