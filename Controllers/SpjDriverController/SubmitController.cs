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
        public IActionResult ProcessStruk(string NomorStruk, string NomorPolisi, string Penugasan, 
            string WaktuMasuk, string WaktuKeluar, int? BeratMasuk, int? BeratKeluar, int BeratNett)
        {
            try
            {
                // Validate required inputs
                if (string.IsNullOrEmpty(NomorStruk) || BeratNett <= 0)
                {
                    TempData["Error"] = "Nomor struk dan berat nett harus diisi.";
                    return RedirectToAction("Struk");
                }

                // Validate receipt number format (numbers only, 7+ digits)
                if (!System.Text.RegularExpressions.Regex.IsMatch(NomorStruk, @"^\d{7,}$"))
                {
                    TempData["Error"] = "Format nomor struk tidak valid. Harus berupa angka minimal 7 digit.";
                    return RedirectToAction("Struk");
                }

                // Validate weight range
                if (BeratNett < 100 || BeratNett > 50000)
                {
                    TempData["Error"] = "Berat nett harus antara 100 kg - 50,000 kg.";
                    return RedirectToAction("Struk");
                }

                // Validate optional weights
                if (BeratMasuk.HasValue && (BeratMasuk < 0 || BeratMasuk > 100000))
                {
                    TempData["Error"] = "Berat masuk tidak valid.";
                    return RedirectToAction("Struk");
                }

                if (BeratKeluar.HasValue && (BeratKeluar < 0 || BeratKeluar > 100000))
                {
                    TempData["Error"] = "Berat keluar tidak valid.";
                    return RedirectToAction("Struk");
                }

                // Here you would normally save to database
                // For now, just simulate success with all data
                var submitData = new
                {
                    NomorStruk,
                    NomorPolisi = NomorPolisi ?? "N/A",
                    Penugasan = Penugasan ?? "N/A",
                    WaktuMasuk = WaktuMasuk ?? "N/A",
                    WaktuKeluar = WaktuKeluar ?? "N/A",
                    BeratMasuk = BeratMasuk?.ToString() ?? "N/A",
                    BeratKeluar = BeratKeluar?.ToString() ?? "N/A",
                    BeratNett
                };
                
                TempData["Success"] = $"Struk berhasil disubmit! No: {NomorStruk}, Nett: {BeratNett} kg";
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
