using Microsoft.AspNetCore.Mvc;

namespace eSPJ.Controllers.SpjDriverController
{
    [Route("submit")]
    public class SubmitController : Controller
    {

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("~/Views/Admin/Transport/SpjDriver/Submit/Index.cshtml");
        }

        
        [HttpGet("struk")]
        public IActionResult Struk()
        {
            return View("~/Views/Admin/Transport/SpjDriver/Submit/Struk.cshtml");
        }
    }
}
