using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Extensions;
using MotorDoctor.Core.Enum;

namespace MotorDoctor.Presentation.Controllers;

public class BranchController : Controller
{
    private readonly IBranchService _branchService;
    private readonly Languages _language;

    public BranchController(IBranchService branchService, ILanguageService languageService)
    {
        _branchService = branchService;
        _language = languageService.SelectedLanguage;
    }

    public async Task<IActionResult> Index()
    {
        var branchs = await _branchService.GetAllAsync(Constants.SelectedLanguage);
        return View(branchs);
    }
}
