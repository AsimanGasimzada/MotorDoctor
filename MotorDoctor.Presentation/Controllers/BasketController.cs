using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Extensions;
using MotorDoctor.Presentation.Extensions;

namespace MotorDoctor.Presentation.Controllers;

public class BasketController : Controller
{
    private readonly IBasketService _service;

    public BasketController(IBasketService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _service.GetBasketAsync(Constants.SelectedLanguage);
        return View(result);
    }
    public async Task<IActionResult> GetBasketSection()
    {
        var basket = await _service.GetBasketAsync();
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
        await _service.AddToBasketAsync(id, count);

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
