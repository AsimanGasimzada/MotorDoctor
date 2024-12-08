using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;
using MotorDoctor.Business.UIServices.Abstractions;
using MotorDoctor.Core.Enum;
using MotorDoctor.Presentation.Extensions;
using Newtonsoft.Json;

namespace MotorDoctor.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly IHomeService _homeService;
    private readonly ILanguageService _languageService;
    private readonly ISubscriberService _subscriberService;
    private readonly Languages _language;
    public HomeController(IHomeService homeService, ILanguageService languageService, ISubscriberService subscriberService)
    {
        _homeService = homeService;
        _languageService = languageService;
        _subscriberService = subscriberService;
        _language = _languageService.RenderSelectedLanguage();
    }

    public async Task<IActionResult> Index()
    {

        var dto = await _homeService.GetHomeDtoAsync(_language);

        return View(dto);
    }

    public IActionResult SelectCulture(string culture)
    {
        _languageService.SelectCulture(culture);

        string returnUrl = Request.GetReturnUrl();

        return Redirect(returnUrl);
    }


    public IActionResult Error(string json)
    {
        if (!string.IsNullOrEmpty(json))
        {
            var dto = JsonConvert.DeserializeObject<ErrorDto>(json);
            return View(dto);
        }

        return View(new ErrorDto
        {
            StatusCode = 500,
            Message = "Gözl?nilm?y?n x?ta ba? verdi."
        });
    }


    public async Task<IActionResult> AddSubscriber(SubscriberCreateDto dto)
    {
        var result = await _subscriberService.CreateAsync(dto, ModelState);

        string returnUrl = Request.GetReturnUrl();

        return Redirect(returnUrl);
    }

}
