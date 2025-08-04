using Microsoft.AspNetCore.Mvc;

namespace eSPJ.Controllers.SpjDriverController
{
      [Route("scan")]
    public class ScanController : Controller
    {

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("~/Views/Admin/Transport/SpjDriver/Scan/Index.cshtml");
        }

        [HttpGet("detail")]
        public IActionResult Detail()
        {
            return View("~/Views/Admin/Transport/SpjDriver/Scan/Detail.cshtml");
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("~/Views/Admin/Transport/SpjDriver/Scan/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> ProcessScan(string barcode)
        {
            try
            {
                if (string.IsNullOrEmpty(barcode))
                {
                    TempData["Error"] = "Kode barcode tidak boleh kosong.";
                    return RedirectToAction("Index");
                }

                if (barcode.Length < 5)
                {
                    TempData["Error"] = "Format kode SPJ tidak valid. Minimal 5 karakter.";
                    return RedirectToAction("Index");
                }

                barcode = barcode.Trim();
                var spjData = await ValidateSpjCode(barcode);

                if (spjData == null)
                {
                    TempData["Error"] = $"SPJ dengan kode '{barcode}' tidak ditemukan.";
                    return RedirectToAction("Index");
                }

                TempData["Success"] = $"SPJ '{barcode}' berhasil ditemukan!";
                return RedirectToAction("Index", "Detail", new { spjCode = barcode });


            }
            catch (Exception)
            {
                TempData["Error"] = "Terjadi kesalahan saat memproses scan. Silakan coba lagi.";
                return RedirectToAction("Index");
            }
        }

        private async Task<SpjData?> ValidateSpjCode(string barcode)
        {

            try
            {
                await Task.Delay(100); 
                if (barcode.ToUpper().StartsWith("SPJ"))
                {
                    return new SpjData
                    {
                        Code = barcode,
                        Status = "Active",
                        Driver = "Sample Driver",
                        Vehicle = "Sample Vehicle"
                    };
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public class SpjData
        {
            public string Code { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public string Driver { get; set; } = string.Empty;
            public string Vehicle { get; set; } = string.Empty;
        }
    }
}
