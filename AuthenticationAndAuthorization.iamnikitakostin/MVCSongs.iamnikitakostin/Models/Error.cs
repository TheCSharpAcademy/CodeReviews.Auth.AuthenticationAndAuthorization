namespace MVCSongs.Models;

public class Error
{
    public int Id { get; set; }
    public string LogLevel { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
}
