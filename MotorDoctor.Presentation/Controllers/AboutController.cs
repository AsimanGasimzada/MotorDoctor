using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Extensions;
using MotorDoctor.Core.Enum;

namespace MotorDoctor.Presentation.Controllers;

public class AboutController : Controller
{
    private readonly IAboutService _aboutService;
    private readonly Languages _language;

    public AboutController(IAboutService aboutService, ILanguageService languageService)
    {
        _aboutService = aboutService;
        _language = languageService.SelectedLanguage;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _aboutService.GetAllAsync(_language);

        return View(result);
    }
}
