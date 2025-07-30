using Microsoft.AspNetCore.Mvc;

namespace eSPJ.Controllers.SpjDriverController
{
    [Route("profil")]
    public class ProfilController : Controller
    {

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("~/Views/Admin/Transport/SpjDriver/Profil/Index.cshtml");
        }
    }
}
