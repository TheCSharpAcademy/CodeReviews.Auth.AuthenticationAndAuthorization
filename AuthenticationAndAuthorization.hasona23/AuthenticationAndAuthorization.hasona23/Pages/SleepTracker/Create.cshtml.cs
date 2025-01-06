using AuthenticationAndAuthorization.hasona23.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthenticationAndAuthorization.hasona23.Components.SleepTracker
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AuthenticationAndAuthorization.hasona23.Data.SleepTrackerContext _context;

        public CreateModel(AuthenticationAndAuthorization.hasona23.Data.SleepTrackerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SleepRecordModel SleepRecordModel { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SleepRecords.Add(SleepRecordModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
