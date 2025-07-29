using Microsoft.AspNetCore.Mvc;

namespace eSPJ.Controllers
{
    [Route("submit")]
    public class SubmitController : Controller
    {

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpGet("struk")]
        public IActionResult Struk()
        {
            return View();
        }
    }
}
