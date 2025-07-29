using Microsoft.AspNetCore.Mvc;

namespace eSPJ.Controllers
{
    [Route("detail-penjemputan")]
    public class DetailPenjemputanController : Controller
    {

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("batal")]
        public IActionResult Batal()
        {
            return View("~/Views/DetailPenjemputan/Batal.cshtml");
        }

    }
}
