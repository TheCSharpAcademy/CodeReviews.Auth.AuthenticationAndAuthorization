using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthenticationAndAuthorization.hasona23.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SleepTrackerContextConnection") ?? throw new InvalidOperationException("Connection string 'SleepTrackerContextConnection' not found.");;

builder.Services.AddDbContext<SleepTrackerContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<LoggerDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    //Lockout options
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;
    //Password options
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    //User Options
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.@";

}).AddEntityFrameworkStores<SleepTrackerContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SleepTrackerContext>();
    context.Database.Migrate();
    DataSeeder.SeedData(context,20,services.GetRequiredService<UserManager<IdentityUser>>());
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapGet("/", context =>
{
    context.Response.Redirect("/SleepTracker/");
    return Task.CompletedTask;
});
app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();