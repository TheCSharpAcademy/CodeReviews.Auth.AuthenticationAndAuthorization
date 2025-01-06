using AuthenticationAndAuthorization.hasona23.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAndAuthorization.hasona23.Components.SleepTracker
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly AuthenticationAndAuthorization.hasona23.Data.SleepTrackerContext _context;

        public IndexModel(AuthenticationAndAuthorization.hasona23.Data.SleepTrackerContext context)
        {
            _context = context;
        }

        public IList<SleepRecordModel> SleepRecordModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SleepRecordModel = await _context.SleepRecords.ToListAsync();
        }
    }
}
