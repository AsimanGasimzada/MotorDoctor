using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;
using MotorDoctor.Business.Exceptions;
using MotorDoctor.Core.Enum;

namespace MotorDoctor.Presentation.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ILanguageService _languageService;
    private readonly Languages _language;


    public OrderController(IOrderService orderService, ILanguageService languageService)
    {
        _orderService = orderService;
        _languageService = languageService;
        _language = _languageService.RenderSelectedLanguage();
    }

    public async Task<IActionResult> Index()
    {
        var result = await _orderService.GetUserUnSubmitOrderAsync(_language);

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Index(OrderCreateDto dto)
    {
        var result = await _orderService.CreateAsync(dto, ModelState);

        if (result is false)
        {
            var order = await _orderService.GetUserUnSubmitOrderAsync(_language);
            dto.OrderItems = order.OrderItems;
            return View(dto);
        }

        return RedirectToAction("Index", "Shop");
    }
    [Authorize]
    public async Task<IActionResult> List()
    {
        var result = await _orderService.GetUserOrdersAsync(_language);

        return View(result);
    }
}


