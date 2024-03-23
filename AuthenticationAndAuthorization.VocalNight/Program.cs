using Microsoft.EntityFrameworkCore;
using MovieMvc.Data;
using MovieMvc.Models;
using Microsoft.AspNetCore.Identity;
using MovieMvc.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<MovieMvcContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database") 
    ?? throw new InvalidOperationException("Connection string 'Database' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MovieMvcContext>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ILogs, LogRepository>();
builder.Services.AddScoped<LogRepository>();

var app = builder.Build();

CreateDb(app);

void CreateDb( WebApplication app )
{

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var db = scope.ServiceProvider.GetRequiredService<MovieMvcContext>();
        db.Database.Migrate();

        SeedData.Initialize(services);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
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
