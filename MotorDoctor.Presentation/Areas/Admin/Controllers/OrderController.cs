using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Extensions;

namespace MotorDoctor.Presentation.Areas.Admin.Controllers;
[Area("Admin")]
public class OrderController : Controller
{
    private readonly IOrderService _service;

    public OrderController(IOrderService orderService)
    {
        _service = orderService;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _service.GetAllAsync(Constants.SelectedLanguage);

        return View(orders);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var result = await _service.GetAsync(id, Constants.SelectedLanguage);

        return View(result);
    }

    public async Task<IActionResult> PrevStatus(int id)
    {
        await _service.PrevOrderStatusAsync(id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> NextStatus(int id)
    {
        await _service.NextOrderStatusAsync(id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> CancelOrder(int id)
    {
        await _service.CancelOrderAsync(id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RepairOrder(int id)
    {
        await _service.RepairOrderAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
