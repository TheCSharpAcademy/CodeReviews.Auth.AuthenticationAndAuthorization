namespace Movies.StevieTV.Logging;

public class DbLoggerOptions
{
    public string ConnectionString { get; init; }  

    public string[] LogFields { get; init; }  

    public string LogTable { get; init; }  

}