namespace MVC_tutorial.Models;

public class ErrorLog
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}