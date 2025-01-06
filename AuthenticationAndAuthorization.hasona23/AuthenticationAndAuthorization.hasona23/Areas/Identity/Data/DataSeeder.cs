using AuthenticationAndAuthorization.hasona23.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAndAuthorization.hasona23.Data;

public static class DataSeeder
{
    public static void SeedData(SleepTrackerContext dbContext,int amount,UserManager<IdentityUser> manager,bool SeedEmptyOnly = true)
    {
        if (dbContext.SleepRecords.Any() && SeedEmptyOnly || !manager.Users.Any())
        {
            Console.WriteLine($"Failed to Seed Data. {dbContext.SleepRecords.Any() && SeedEmptyOnly }: {!manager.Users.Any()}");
            return;
        }
        
        Random random = new Random();
        for (int i = 0; i < amount; i++)
        {
            var startTimeRand = new DateTime(2025, random.Next(1, 13), random.Next(1, 25), random.Next(20, 22), 00, 00);
            var user = manager.Users.Take(1).FirstOrDefault();
            SleepRecordModel sleepRecord = new SleepRecordModel
            {
                StartTime = startTimeRand,
                EndTime = new DateTime(startTimeRand.Year, startTimeRand.Month, startTimeRand.Day+1,random.Next(4,9), startTimeRand.Minute, startTimeRand.Second),
                User =user,
            };
            dbContext.SleepRecords.Add(sleepRecord);
        }

        dbContext.SaveChanges();
    }
}