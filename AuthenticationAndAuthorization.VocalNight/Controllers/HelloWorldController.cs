using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MovieMvc.Controllers
{
    public class HelloWorldController : Controller
    {
        //Get: /Helloworld/
        public IActionResult Index()
        {
            return View();
        }

        // Get: /helloworld/welcome
        public IActionResult Welcome(string name, int ID = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = ID;
            return View();
        }
    }
}
