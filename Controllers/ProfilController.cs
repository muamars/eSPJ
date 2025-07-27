using Microsoft.AspNetCore.Mvc;

namespace eSPJ.Controllers
{
    [Route("profil")]
    public class ProfilController : Controller
    {

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

       
    }
}
