using Budget.Application.Installers;
using Budget.Infrastructure.Installers;
using Budget.Web.Installers;

namespace Budget.Web;

/// <summary>
/// The entry point for the Presentation layer.
/// This class is responsible for configuring and launching the application.
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddWeb();

        var app = builder.Build();
        app.AddMiddleware();
        app.SetUpDatabase();
        app.Run();
    }
}
