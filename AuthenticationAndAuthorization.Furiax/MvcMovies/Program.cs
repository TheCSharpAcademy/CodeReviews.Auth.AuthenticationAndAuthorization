using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using MvcMovies.Areas.Identity.Data;
using MvcMovies.Data;
using MvcMovies.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//Add DbContext registration
builder.Services.AddDbContext<MoviesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesContext") ?? throw new InvalidOperationException("Connection string 'MoviesContext' not found.")));
//scaffolding didn't recognize my context file, so had to add a second scaffolded context
builder.Services.AddDbContext<MvcMoviesContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesContext")));

//Add Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MvcMoviesContext>();

// Add services to the container.
builder.Services.AddSingleton<DbLogger>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

//create database and seed data
CreateDatabases(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


var defaultCulture = new CultureInfo("nl-BE");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = new List<CultureInfo> { defaultCulture },
    SupportedUICultures = new List<CultureInfo> { defaultCulture }
};
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

void CreateDatabases(WebApplication app)
{

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var moviesContext = services.GetRequiredService<MoviesContext>();
        var mvcMoviesContext = services.GetRequiredService<MvcMoviesContext>();

        moviesContext.Database.Migrate();
        mvcMoviesContext.Database.Migrate();

        SeedData.Initialize(services);
        SeedDataMusic.Initialize(services);
    }
}