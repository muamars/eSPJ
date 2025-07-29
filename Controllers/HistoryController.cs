using Microsoft.AspNetCore.Mvc;

namespace eSPJ.Controllers
{
    [Route("history")]
    public class HistoryController : Controller
    {

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            ViewData["Id"] = id;
            return View();
        }
    }
}
