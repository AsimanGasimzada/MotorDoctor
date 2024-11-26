using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;
using MotorDoctor.Business.UIServices.Abstractions;
using MotorDoctor.Core.Enum;

namespace MotorDoctor.Presentation.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;
    private readonly ILanguageService _languageService;
    private readonly Languages _language;

    public ContactController(IContactService contactService, ILanguageService languageService)
    {
        _contactService = contactService;
        _languageService = languageService;
        _language= _languageService.RenderSelectedLanguage();
    }

    public async Task<IActionResult> Index()
    {

        var result = await _contactService.GetContactDtoAsync(_language);

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Index(ContactDto dto)
    {
        var result = await _contactService.SendEmailAsync(dto, ModelState);

        return RedirectToAction(nameof(Index));

    }
}
