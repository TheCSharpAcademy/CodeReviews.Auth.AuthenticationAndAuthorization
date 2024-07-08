using Microsoft.Build.Logging;
using Microsoft.Extensions.Logging;
using System.IO;

namespace RadiologyPatientsExams.Data;

public class Logger : ILogger
{

    private readonly string _filePath;
    private static readonly object _lock = new object();

    public Logger(string filePath)
    {
        _filePath = filePath;
    }


    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => logLevel == LogLevel.Information;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (formatter != null)
        {
            lock (_lock)
            {
                File.AppendAllText(_filePath, formatter(state, exception) + Environment.NewLine);
            }
        }
    }
}

public class FileLoggerProvider : ILoggerProvider
{
    private readonly string _filePath;

    public FileLoggerProvider(string filePath)
    {
        _filePath = filePath;
    }

    public ILogger CreateLogger(string categoryName) => new Logger(_filePath);

    public void Dispose() { }
}

