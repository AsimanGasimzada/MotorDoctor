using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;
using MotorDoctor.Business.Extensions;

namespace MotorDoctor.Presentation.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;


    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var result = await _orderService.GetUserUnSubmitOrderAsync(Constants.SelectedLanguage);

        return View(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Index(OrderCreateDto dto)
    {
        var result = await _orderService.CreateAsync(dto, ModelState);

        if (result is false)
        {
            var order = await _orderService.GetUserUnSubmitOrderAsync(Constants.SelectedLanguage);
            dto.OrderItems = order.OrderItems;
            return View(dto);
        }

        return RedirectToAction("Index", "Shop");
    }
}
