using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;

namespace MotorDoctor.Presentation.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class EmailController : Controller
{
    private readonly ISubscriberService _service;

    public EmailController(ISubscriberService service)
    {
        _service = service;
    }

    public IActionResult Index(bool isSuccess = false)
    {
        ViewBag.IsSuccess = isSuccess;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(SubscriberEmailDto dto)
    {
        var result = await _service.SendEmailToSubscribres(dto, ModelState);

        if (result is false)
            return View(dto);


        return RedirectToAction(nameof(Index), routeValues: new { isSuccess = true });
    }
}
