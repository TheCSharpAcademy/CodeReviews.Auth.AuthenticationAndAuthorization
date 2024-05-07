using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Serilog.Templates;
using Serilog.Templates.Themes;
using Serilog.Sinks.MSSqlServer;
using Serilog.Configuration;

Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesConnectionString") ?? throw new InvalidOperationException("Connection string 'MovieContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MovieContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddSerilog((services, lc) => lc
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console(new ExpressionTemplate(
        // Include trace and span ids when present.
        "[{@t:HH:mm:ss} {@l:u3}{#if @tr is not null} ({substring(@tr,0,4)}:{substring(@sp,0,4)}){#end}] {@m}\n{@x}",
        theme: TemplateTheme.Code))
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("MoviesConnectionString") ?? throw new InvalidOperationException("Connection string 'MovieContext' not found."),
        sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlDatabase = true, AutoCreateSqlTable = true },
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
        ));



var app = builder.Build();
app.UseSerilogRequestLogging();
app.UseRequestLocalization( new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture("en-US")
    }
);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.InitializeUser(services).GetAwaiter().GetResult();
    SeedData.InitializeMovies(services);
    SeedData.InitializeTvShows(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

app.Run();
