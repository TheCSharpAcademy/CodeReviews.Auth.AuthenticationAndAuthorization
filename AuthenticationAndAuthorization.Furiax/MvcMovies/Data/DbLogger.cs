using Microsoft.Data.SqlClient;

namespace MvcMovies.Data
{
    public class DbLogger : ILogger
    {
        private readonly string _connectionString;
        public DbLogger(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MoviesContext");
        }
        public IDisposable? BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= LogLevel.Error;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (logLevel >= LogLevel.Error && exception != null)
            {
                LogToDatabase(logLevel, exception.Message, exception.StackTrace);
            }
        }
        public void LogError(Exception exception)
        {
            if (exception != null)
            {
                LogToDatabase(LogLevel.Error, exception.Message, exception.StackTrace);
            }
        }
        private void LogToDatabase(LogLevel logLevel, string errorMessage, string stackTrace)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO ErrorLog (LogLevel, ErrorMessage, StackTrace, LogTime) VALUES (@LogLevel, @ErrorMessage, @StackTrace, @LogTime)";
                    command.Parameters.AddWithValue("@LogLevel", logLevel.ToString());
                    command.Parameters.AddWithValue("@ErrorMessage", errorMessage);
                    command.Parameters.AddWithValue("@StackTrace", stackTrace);
                    command.Parameters.AddWithValue("@LogTime", DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
