using Microsoft.EntityFrameworkCore;
using MVCSongs.Data;
using MVCSongs.Models;
using Microsoft.AspNetCore.Identity;
using MVCSongs.Log;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MVCSongsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MVCSongsContext") ?? throw new InvalidOperationException("Connection string 'MVCSongsContext' not found.")));

builder.Logging.ClearProviders();
builder.Logging.AddProvider(new DatabaseLogProvider(builder.Services.BuildServiceProvider().GetRequiredService<MVCSongsContext>()));
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MVCSongsContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MVCSongsContext>();
    dbContext.Database.EnsureCreated();

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

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapRazorPages();

app.Run();
