using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RadiologyPatientsExams.Data;
using RadiologyPatientsExams.Models;
using RadiologyPatientsExams.Repositories;
using RadiologyPatientsExams.Services;

namespace RadiologyPatientsExams
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();


            string contentRootPath = builder.Environment.ContentRootPath;
            string logDirectory = Path.Combine(contentRootPath, "Logs");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            string logFilePath = Path.Combine(logDirectory, "LogOutput.txt");
            builder.Logging.AddProvider(new FileLoggerProvider(logFilePath));


          


            builder.Services.AddScoped<IRadiologyPatientRepository, PatientRepository>();
            builder.Services.AddScoped<PatientService>();
            builder.Services.AddScoped<Validations.BirthDateValidation>();

            builder.Services.AddScoped<IRadiologyExamRepository, ExamRepository>();
            builder.Services.AddScoped<ExamService>();

            builder.Services.AddScoped<Seeder>();

            builder.Services.AddDbContextFactory<RadiologyDb>(
              options => options.UseSqlServer(builder.Configuration.GetConnectionString("RadiologyDb") ?? throw new InvalidOperationException("Connection string 'RadiologyDb' not found."))
                                .UseLoggerFactory(LoggerFactory.Create(builder =>
                                {
                                    builder.AddProvider(new FileLoggerProvider(logFilePath));
                                })));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = false;
            })
             .AddEntityFrameworkStores<RadiologyDb>()
              .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.HttpOnly = true;
            });


            builder.Services.AddRazorPages(); 

           var app = builder.Build();


            var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<RadiologyDb>();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });

         
            

            app.Run();
        }


    }
}
