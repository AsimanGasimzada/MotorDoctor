using Microsoft.AspNetCore.Mvc;

namespace MotorDoctor.Presentation.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
