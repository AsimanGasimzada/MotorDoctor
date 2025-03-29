using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Extensions;
using MotorDoctor.Core.Enum;
using MotorDoctor.Presentation.Extensions;

namespace MotorDoctor.Presentation.Controllers;

public class BasketController : Controller
{
    private readonly IBasketService _service;
    private readonly Languages _language;

    public BasketController(IBasketService service, ILanguageService languageService)
    {
        _service = service;
        _language = languageService.SelectedLanguage;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _service.GetBasketAsync(_language);
        return View(result);
    }
    public async Task<IActionResult> GetBasketSection()
    {
        var basket = await _service.GetBasketAsync(_language);
        return PartialView("_basketSectionPartial", basket);
    }
    public async Task<IActionResult> RemoveToBasket(int id)
    {
        await _service.RemoveToBasketAsync(id);

        string returnUrl = Request.GetReturnUrl();

        return Redirect(returnUrl);
    }

    public async Task<IActionResult> AddToBasket(int id, int count = 1)
    {
        var result = await _service.AddToBasketAsync(id, count);

        TempData["BasketResult"] = result ? "success" : "error";

        return RedirectToAction(nameof(RedirectForBasket));
    }
    public IActionResult RedirectForBasket()
    {
        return PartialView("_basketModalPartial");
    }

    public async Task<IActionResult> DecreaseToBasket(int id)
    {
        await _service.DecreaseToBasketAsync(id);

        return RedirectToAction(nameof(RedirectForBasket));
    }
}
