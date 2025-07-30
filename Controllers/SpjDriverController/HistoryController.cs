using Microsoft.AspNetCore.Mvc;

namespace eSPJ.Controllers.SpjDriverController
{
    [Route("history")]
    public class HistoryController : Controller
    {

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("~/Views/Admin/Transport/SpjDriver/History/Index.cshtml");
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            ViewData["Id"] = id;
            return View("~/Views/Admin/Transport/SpjDriver/History/Details.cshtml");
        }
    }
}
