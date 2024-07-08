using Microsoft.EntityFrameworkCore;
using RadiologyPatientsExams.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace RadiologyPatientsExams.Data
{
    public class RadiologyDb : IdentityDbContext<ApplicationUser>
    {
        public RadiologyDb(DbContextOptions<RadiologyDb> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder =>
                {
                    builder.AddProvider(new FileLoggerProvider(@"C:\Temp\LogOutput.txt"));
                }));
            }
        }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<RadiologyPatientsExams.Models.ExamViewModel> ExamViewModel { get; set; } = default!;
    }
}
