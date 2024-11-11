using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace MotorDoctor.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public HomeController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            await _basketService.AddToBasketAsync(65);

            await _orderService.CreateAsync(new() { City = "Test", PhoneNumber = "Test", Region = "Test", Street = "Test" , OrderItems = [] }, ModelState);

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
