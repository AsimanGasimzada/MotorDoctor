using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;
using MotorDoctor.Business.Services.Abstractions;
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
    private readonly ISliderService _sliderService;
    private readonly IAdvertisementService _advertisementService;
    public HomeController(IHomeService homeService, ILanguageService languageService, ISubscriberService subscriberService, ISliderService sliderService, IAdvertisementService advertisementService)
    {
        _homeService = homeService;
        _languageService = languageService;
        _subscriberService = subscriberService;
        _language = _languageService.RenderSelectedLanguage();
        _sliderService = sliderService;
        _advertisementService = advertisementService;
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


    public IActionResult Error(string? json)
    {
        if (!string.IsNullOrEmpty(json))
        {

            string decodedJson = Uri.UnescapeDataString(json);

            var dto = JsonConvert.DeserializeObject<ErrorDto>(decodedJson);
            return View(dto);
        }

        return View(new ErrorDto
        {
            StatusCode = 500,
            Message = "Gözlənilməyən xəta baş verdi."
        });
    }


    public async Task<IActionResult> AddSubscriber(SubscriberCreateDto dto)
    {
        try
        {
            var result = await _subscriberService.CreateAsync(dto, ModelState);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        string returnUrl = Request.GetReturnUrl();

        return Redirect(returnUrl);
    }

    public IActionResult ClearCache()
    {
        _homeService.ClearInMemoryCache();

        string returnUrl = Request.GetReturnUrl();

        return Redirect(returnUrl);

    }

    public async Task<IActionResult> SliderRedirect(int id)
    {
        var slider = await _sliderService.GetAsync(id);

        if (!string.IsNullOrWhiteSpace(slider.ButtonPath))
            return Redirect(slider.ButtonPath);

        return RedirectToAction("Index", "Shop");
    }

    public async Task<IActionResult> AdvertisementRedirect(int id)
    {
        var advertisement = await _advertisementService.GetAsync(id);

        return Redirect(advertisement.Url);
    }
}
