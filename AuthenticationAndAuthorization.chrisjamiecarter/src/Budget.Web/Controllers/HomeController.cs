using System.Diagnostics;
using Budget.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Budget.Web.Controllers;

/// <summary>
/// Manages the error handling for the Presentation layer. 
/// This controller is only responsible for providing an error view for unexpected issues.
/// </summary>
public class HomeController : Controller
{
    #region Methods

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    #endregion
}
