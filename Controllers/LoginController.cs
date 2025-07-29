using Microsoft.AspNetCore.Mvc;


namespace eSPJ.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.SSOLoginUrl = _configuration["SSO:LoginUrl"];
            return View();
        }
    }
}
