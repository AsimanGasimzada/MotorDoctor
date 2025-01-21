using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;
using MotorDoctor.Core.Enum;

namespace MotorDoctor.Presentation.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ILanguageService _languageService;
    private readonly Languages _language;
    private readonly IPaymentService _paymentService;


    public OrderController(IOrderService orderService, ILanguageService languageService, IPaymentService paymentService)
    {
        _orderService = orderService;
        _languageService = languageService;
        _language = _languageService.RenderSelectedLanguage();
        _paymentService = paymentService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _orderService.GetUserUnSubmitOrderAsync(_language);

        return View(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(OrderCreateDto dto)
    {
        var result = await _orderService.CreateAsync(dto, ModelState);

        if (result is false)
        {
            var order = await _orderService.GetUserUnSubmitOrderAsync(_language);
            dto.OrderItems = order.OrderItems;
            return View(dto);
        }

        return RedirectToAction("Redirect");
    }

    public IActionResult Redirect()
    {
        string? url = Request.Cookies["paymentUrl"];
        if (!string.IsNullOrWhiteSpace(url))
        {
            Response.Cookies.Delete("paymentUrl");

            return Redirect(url);
        }

        return RedirectToAction("Index", "Shop");
    }
    [Authorize]
    public async Task<IActionResult> List()
    {
        var result = await _orderService.GetUserOrdersAsync(_language);

        return View(result);
    }

    public async Task<IActionResult> TestPayment()
    {
        var result = await _paymentService.CreateAsync(new() { Amount = 100, Description = "Salam", Token = Guid.NewGuid().ToString(), OrderId = 40 });
        var order = result.Order;

        Response.Redirect($"{order.HppUrl}?id={order.Id}&password={order.Password}");

        return Redirect($"{order.HppUrl}?id={order.Id}&password={order.Password}");
    }


    public async Task<IActionResult> CheckPayment(PaymentCheckDto dto)
    {
        var result = await _paymentService.CheckPaymentAsync(dto);

        return RedirectToAction("Index", "Shop");
    }
}


