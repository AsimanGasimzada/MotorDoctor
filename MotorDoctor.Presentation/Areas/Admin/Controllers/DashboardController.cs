using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MotorDoctor.Presentation.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
   private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task<IActionResult> Index()
    {
        var result=await _dashboardService.GetDashboardAsync();

        return View(result);
    }
}
