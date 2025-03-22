using MVCSongs.Data;

namespace MVCSongs.Log;

public class DatabaseLogProvider : ILoggerProvider
{
    private readonly MVCSongsContext _context;

    public DatabaseLogProvider(MVCSongsContext context)
    {
        _context = context;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new DatabaseLogger(_context);
    }

    public void Dispose()
    {

    }
}
