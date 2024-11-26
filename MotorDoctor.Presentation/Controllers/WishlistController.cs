using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Core.Enum;
using MotorDoctor.Presentation.Extensions;

namespace MotorDoctor.Presentation.Controllers;

public class WishlistController : Controller
{
    private readonly IWishlistService _wishlistService;
    private readonly ILanguageService _languageService;
    private readonly Languages _language;

    public WishlistController(IWishlistService wishlistService, ILanguageService languageService)
    {
        _wishlistService = wishlistService;
        _languageService = languageService;
        _language = _languageService.RenderSelectedLanguage();
    }

    public async Task<IActionResult> Index()
    {
        var wishlistItems = await _wishlistService.GetWishlistAsync(_language);

        return View(wishlistItems);
    }
    public async Task<IActionResult> RemoveItem(int id)
    {
        var result = await _wishlistService.ToggleToWishlistAsync(id);

        string returnUrl = Request.GetReturnUrl();

        return Redirect(returnUrl);
    }


    public async Task<IActionResult> AddItem(int id)
    {
        var result = await _wishlistService.ToggleToWishlistAsync(id);

        string returnUrl = Request.GetReturnUrl();

        return Redirect(returnUrl);
    }
}
