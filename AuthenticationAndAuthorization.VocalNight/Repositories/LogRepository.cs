using MovieMvc.Data;
using MovieMvc.Models;

namespace MovieMvc.Repositories
{
    public class LogRepository : ILogs
    {
        private MovieMvcContext _context;

        public LogRepository(MovieMvcContext context)
        {
            _context = context;
        }

        public void Log( string type, string message )
        {
            _context.Logs.Add(new Logs { Category = type, Description = message, Date = new DateTime().Date});
            _context.SaveChangesAsync();
        }
    }
}
