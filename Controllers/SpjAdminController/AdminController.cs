using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eSPJ.Models;

namespace eSPJ.Controllers.SpjAdminController;

[Route("admin")]
public class AdminController : Controller
{

    [HttpGet("")]
    public IActionResult Index()
    {
        return View("~/Views/Admin/Transport/SpjAdmin/Home/Index.cshtml");
    }

    [HttpGet("scan")]
    public IActionResult Scan()
    {
        return View("~/Views/Admin/Transport/SpjAdmin/Scan/Index.cshtml");
    }

    [HttpGet("history")]
    public IActionResult History()
    {
        return View("~/Views/Admin/Transport/SpjAdmin/History/Index.cshtml");
    }
}
