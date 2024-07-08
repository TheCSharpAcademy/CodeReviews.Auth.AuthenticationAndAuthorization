using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RadiologyPatientsExams.Data;
using RadiologyPatientsExams.Models;
using System.Diagnostics;

namespace RadiologyPatientsExams.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Seeder _seeder;

        public HomeController(ILogger<HomeController> logger, Seeder seeder)
        {
            _logger = logger;
            _seeder = seeder;
            seeder.SeedPatients();
            seeder.SeedExams();
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
