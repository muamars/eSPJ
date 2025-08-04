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

        [HttpPost("struk")]
        public IActionResult ProcessStruk(string NomorStruk, string BeratMuatan)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrEmpty(NomorStruk) || string.IsNullOrEmpty(BeratMuatan))
                {
                    TempData["Error"] = "Nomor struk dan berat muatan harus diisi.";
                    return RedirectToAction("Struk");
                }

                if (NomorStruk.Length < 6)
                {
                    TempData["Error"] = "Nomor struk minimal 6 digit.";
                    return RedirectToAction("Struk");
                }

                if (!decimal.TryParse(BeratMuatan, out decimal berat) || berat <= 0)
                {
                    TempData["Error"] = "Berat muatan harus berupa angka yang valid.";
                    return RedirectToAction("Struk");
                }

                // Here you would normally save to database
                // For now, just simulate success
                
                TempData["Success"] = $"Struk berhasil disubmit! No: {NomorStruk}, Berat: {BeratMuatan} kg";
                return RedirectToAction("Index", "Home");

            }
            catch (Exception)
            {
                TempData["Error"] = "Terjadi kesalahan saat memproses struk. Silakan coba lagi.";
                return RedirectToAction("Struk");
            }
        }
    }
}
