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
                // Validate barcode
                if (string.IsNullOrEmpty(barcode))
                {
                    TempData["Error"] = "Kode barcode tidak boleh kosong.";
                    return RedirectToAction("Index");
                }

                // Basic validation for SPJ barcode format
                if (barcode.Length < 5)
                {
                    TempData["Error"] = "Format kode SPJ tidak valid. Minimal 5 karakter.";
                    return RedirectToAction("Index");
                }

                // Clean the barcode (remove any whitespace)
                barcode = barcode.Trim();

                // TODO: Add your SPJ validation logic here
                // For example:
                // - Check if SPJ exists in database
                // - Validate SPJ format according to your business rules
                // - Check SPJ status (active, completed, etc.)

                // Simulate SPJ lookup (replace with your actual database logic)
                var spjData = await ValidateSpjCode(barcode);

                if (spjData == null)
                {
                    TempData["Error"] = $"SPJ dengan kode '{barcode}' tidak ditemukan.";
                    return RedirectToAction("Index");
                }

                // Success - redirect to detail page or next step
                TempData["Success"] = $"SPJ '{barcode}' berhasil ditemukan!";

                // Redirect to appropriate page based on your workflow
                // For example, redirect to detail page:
                return RedirectToAction("Index", "Detail", new { spjCode = barcode });

                // Or redirect to submission page:
                // return RedirectToAction("Index", "Submit", new { spjCode = barcode });

            }
            catch (Exception)
            {
                // Log the error (add your logging here)
                TempData["Error"] = "Terjadi kesalahan saat memproses scan. Silakan coba lagi.";
                return RedirectToAction("Index");
            }
        }

        private async Task<SpjData?> ValidateSpjCode(string barcode)
        {
            // TODO: Implement your SPJ validation logic here
            // This is just a sample implementation

            try
            {
                // Simulate database lookup
                await Task.Delay(100); // Simulate async operation

                // Example validation rules:
                // 1. Check format (e.g., starts with "SPJ" or specific pattern)
                // 2. Check if exists in database
                // 3. Check status

                // For demo purposes, accept codes that start with "SPJ"
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

                // Return null if not found or invalid
                return null;
            }
            catch
            {
                return null;
            }
        }

        // Sample model for SPJ data (replace with your actual model)
        public class SpjData
        {
            public string Code { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public string Driver { get; set; } = string.Empty;
            public string Vehicle { get; set; } = string.Empty;
        }
    }
}
