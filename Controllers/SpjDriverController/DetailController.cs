using Microsoft.AspNetCore.Mvc;

namespace eSPJ.Controllers.SpjDriverController
{
    [Route("detail-penjemputan")]
    public class DetailPenjemputanController : Controller
    {

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("~/Views/Admin/Transport/SpjDriver/DetailPenjemputan/Index.cshtml");
        }

        [HttpGet("batal")]
        public IActionResult Batal()
        {
            return View("~/Views/Admin/Transport/SpjDriver/DetailPenjemputan/Batal.cshtml");
        }

    }
}
