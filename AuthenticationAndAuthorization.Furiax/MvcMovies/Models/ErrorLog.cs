namespace MvcMovies.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string? LogLevel { get; set; }
        public string? ErrorMessage { get; set; }
        public string?  StackTrace { get; set; }
        public DateTime LogTime { get; set; }
    }
}
