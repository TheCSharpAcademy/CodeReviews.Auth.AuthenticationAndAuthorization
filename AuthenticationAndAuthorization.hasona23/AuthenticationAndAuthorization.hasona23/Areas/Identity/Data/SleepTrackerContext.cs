using AuthenticationAndAuthorization.hasona23.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAndAuthorization.hasona23.Data;

public class SleepTrackerContext : IdentityDbContext<IdentityUser>
{
    public SleepTrackerContext(DbContextOptions<SleepTrackerContext> options)
        : base(options)
    {
    }
    public DbSet<SleepRecordModel> SleepRecords { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<SleepRecordModel>().HasOne<IdentityUser>(x => x.User);
    }
}
