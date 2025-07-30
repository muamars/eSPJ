using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eSPJ.Models;

namespace eSPJ.Controllers.SpjDriverController;

public class HomeController : Controller
{
    
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
         return View("~/Views/Admin/Transport/SpjDriver/Home/Index.cshtml");
    }

    public IActionResult Privacy()
    {
        return View("~/Views/Admin/Transport/SpjDriver/Home/Privacy.cshtml");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
