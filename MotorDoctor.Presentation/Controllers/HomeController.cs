using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;
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
        private readonly ISubscriberService _subscriberService;
        public HomeController(IBasketService basketService, IOrderService orderService, IHomeService homeService, ILanguageService languageService, ISubscriberService subscriberService)
        {
            _basketService = basketService;
            _orderService = orderService;
            _homeService = homeService;
            _languageService = languageService;
            _subscriberService = subscriberService;
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



        public async Task<IActionResult> AddSubscriber(SubscriberCreateDto dto)
        {
            var result = await _subscriberService.CreateAsync(dto, ModelState);

            string returnUrl = Request.GetReturnUrl();

            return Redirect(returnUrl);
        }

    }
}
