using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Business.Dtos;
using MotorDoctor.Presentation.Extensions;

namespace MotorDoctor.Presentation.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
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
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto, ModelState);

        if (result is false)
            return View(dto);

        return RedirectToAction(nameof(Login));
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
}
