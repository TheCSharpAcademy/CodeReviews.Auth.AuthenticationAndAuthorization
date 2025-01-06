using AuthenticationAndAuthorization.hasona23.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAndAuthorization.hasona23.Components.SleepTracker
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly AuthenticationAndAuthorization.hasona23.Data.SleepTrackerContext _context;

        public DeleteModel(AuthenticationAndAuthorization.hasona23.Data.SleepTrackerContext context)
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

            var sleeprecordmodel = await _context.SleepRecords.FirstOrDefaultAsync(m => m.Id == id);

            if (sleeprecordmodel is not null)
            {
                SleepRecordModel = sleeprecordmodel;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sleeprecordmodel = await _context.SleepRecords.FindAsync(id);
            if (sleeprecordmodel != null)
            {
                SleepRecordModel = sleeprecordmodel;
                _context.SleepRecords.Remove(SleepRecordModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
