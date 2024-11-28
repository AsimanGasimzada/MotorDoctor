using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Extensions;

namespace MotorDoctor.Presentation.Controllers;

public class BranchController : Controller
{
    private readonly IBranchService _branchService;
    private readonly ILanguageService _languageService;


    public BranchController(IBranchService branchService, ILanguageService languageService)
    {
        _branchService = branchService;
        _languageService = languageService;
        _languageService.RenderSelectedLanguage();
    }

    public async Task<IActionResult> Index()
    {
        var branchs = await _branchService.GetAllAsync(Constants.SelectedLanguage);
        return View(branchs);
    }
}
