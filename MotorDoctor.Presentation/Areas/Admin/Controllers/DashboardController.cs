using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MotorDoctor.Presentation.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly IAuthService _authService;
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService, IAuthService authService)
    {
        _dashboardService = dashboardService;
        _authService = authService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _dashboardService.GetDashboardAsync();

        return View(result);
    }

    public async Task<IActionResult> RemoveBots()
    {
        await _authService.RemoveBotsAsync();

        return Content("Done");
    }
}
