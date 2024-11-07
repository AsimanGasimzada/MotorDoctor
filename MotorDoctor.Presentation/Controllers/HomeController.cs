using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace MotorDoctor.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SelectCulture(string culture)
        {
            Console.WriteLine("Selected lang " + culture);

            if (!string.IsNullOrEmpty(culture))
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTime.UtcNow.AddYears(1) }
                    );
            }

            string returnUrl = Request.Headers["Referer"];

            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/";

            return Redirect(returnUrl);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
