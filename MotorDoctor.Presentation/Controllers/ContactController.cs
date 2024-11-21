using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;
using MotorDoctor.Business.UIServices.Abstractions;

namespace MotorDoctor.Presentation.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _contactService.GetContactDtoAsync();

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Index(ContactDto dto)
    {
        var result = await _contactService.SendEmailAsync(dto, ModelState);

        return RedirectToAction(nameof(Index));

    }
}
