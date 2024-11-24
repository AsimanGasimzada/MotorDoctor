using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Extensions;

namespace MotorDoctor.Presentation.Controllers;

public class AboutController : Controller
{
    private readonly IAboutService aboutService;

    public AboutController(IAboutService aboutService)
    {
        this.aboutService = aboutService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await aboutService.GetAllAsync(Constants.SelectedLanguage);

        return View(result);
    }
}
