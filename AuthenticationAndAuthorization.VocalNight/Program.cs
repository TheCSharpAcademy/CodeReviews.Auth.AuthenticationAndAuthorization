using Microsoft.EntityFrameworkCore;
using MovieMvc.Data;
using MovieMvc.Models;
using Microsoft.AspNetCore.Identity;
using MovieMvc.Repositories;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MovieMvcContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieMvcContext") 
    ?? throw new InvalidOperationException("Connection string 'MovieMvcContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MovieMvcContext>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ILogs, LogRepository>();
builder.Services.AddScoped<LogRepository>();


var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();

    builder.SetMinimumLevel(LogLevel.Information);
});

var logger = loggerFactory.CreateLogger<MovieMvcContext>();
logger.LogInformation("Application starting");


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = scope.ServiceProvider.GetRequiredService<MovieMvcContext>();
    db.Database.EnsureDeleted();

}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
