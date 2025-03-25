using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;
using MotorDoctor.Core.Enum;
using MotorDoctor.DataAccess.Localizers;
using MotorDoctor.Presentation.Extensions;

namespace MotorDoctor.Presentation.Controllers;

[AutoValidateAntiforgeryToken]
public class AccountController : Controller
{
    private readonly IAuthService _authService;
    private readonly Languages _language;
    private readonly AccountLocalizer _localizer;

    public AccountController(IAuthService authService, ILanguageService languageService, AccountLocalizer localizer)
    {
        _authService = authService;
        _language = languageService.SelectedLanguage;
        _localizer = localizer;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto, ModelState);

        if (result is false)
            return View(dto);

        if (!string.IsNullOrWhiteSpace(dto.ReturnUrl))
            return Redirect(dto.ReturnUrl);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto, ModelState);

        if (result is false)
            return View(dto);

        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();

        var returnUrl = Request.GetReturnUrl();

        return Redirect(returnUrl);
    }


    public async Task<IActionResult> VerifyEmail(VerifyEmailDto dto)
    {
        var result = await _authService.VerifyEmailAsync(dto, ModelState);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
    {
        var result = await _authService.SendForgotPasswordEmailAsync(dto, ModelState);

        if (result is false)
            return View(dto);

        TempData["SuccedAlert"] = _localizer.GetValue("SuccedForgotPassword");


        return RedirectToAction(nameof(Login));
    }

    public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
    {
        var result = await _authService.CheckResetPasswordTokenAsync(dto);

        if (result is false)
            return RedirectToAction(nameof(Login));

        return View(dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("ResetPassword")]
    public async Task<IActionResult> SubmitResetPassword(ResetPasswordDto dto)
    {
        var result = await _authService.ResetPasswordAsync(dto, ModelState);

        if (result is false)
            return View(dto);

        return RedirectToAction("Index", "Home");
    }
}
