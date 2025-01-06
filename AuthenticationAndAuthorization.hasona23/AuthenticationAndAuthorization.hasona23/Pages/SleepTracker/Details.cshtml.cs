using AuthenticationAndAuthorization.hasona23.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAndAuthorization.hasona23.Components.SleepTracker
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly AuthenticationAndAuthorization.hasona23.Data.SleepTrackerContext _context;

        public DetailsModel(AuthenticationAndAuthorization.hasona23.Data.SleepTrackerContext context)
        {
            _context = context;
        }

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
    }
}
