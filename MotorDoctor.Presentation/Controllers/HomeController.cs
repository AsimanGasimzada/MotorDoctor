using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Extensions;
using MotorDoctor.Business.UIServices.Abstractions;
using MotorDoctor.Presentation.Extensions;
using System.Reflection.Metadata;

namespace MotorDoctor.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;
        private readonly IHomeService _homeService;
        private readonly ILanguageService _languageService;
        public HomeController(IBasketService basketService, IOrderService orderService, IHomeService homeService, ILanguageService languageService)
        {
            _basketService = basketService;
            _orderService = orderService;
            _homeService = homeService;
            _languageService = languageService;
        }

        public async Task<IActionResult> Index()
        {
            var dto = await _homeService.GetHomeDtoAsync(Constants.SelectedLanguage);

            return View(dto);
        }

        public IActionResult SelectCulture(string culture)
        {
            _languageService.SelectCulture(culture);

            string returnUrl = Request.GetReturnUrl();

            return Redirect(returnUrl);
        }



        public IActionResult Privacy()
        {
            return View();
        }

    }
}
