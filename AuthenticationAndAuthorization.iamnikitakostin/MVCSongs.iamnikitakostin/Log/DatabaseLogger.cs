using MVCSongs.Data;

namespace MVCSongs.Log;

public class DatabaseLogger : ILogger
{
    private readonly MVCSongsContext _context;

    public DatabaseLogger(MVCSongsContext context)
    {
        _context = context;
    }

    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= LogLevel.Information;
    }

    public void  Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (formatter == null)
            return;

        string logMessage = formatter(state, exception);

        _context.Errors.Add(new Models.Error
        {
            LogLevel = logLevel.ToString(),
            Message = logMessage,
            Timestamp = DateTime.Now
        });

        _context.SaveChanges();

        Console.WriteLine($"[{logLevel}] {logMessage}");
    }
}
