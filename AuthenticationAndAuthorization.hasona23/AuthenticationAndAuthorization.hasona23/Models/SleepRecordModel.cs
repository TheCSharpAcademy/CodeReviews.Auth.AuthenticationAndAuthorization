using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationAndAuthorization.hasona23.Models;

public class SleepRecordModel
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration => EndTime - StartTime; 
    //Navigation
    public IdentityUser User { get; set; }
}