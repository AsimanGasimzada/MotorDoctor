using Microsoft.AspNetCore.Mvc;
using MotorDoctor.Presentation.Extensions;

namespace MotorDoctor.Presentation.Controllers;

public class WishlistController : Controller
{
    private readonly IWishlistService _wishlistService;

    public WishlistController(IWishlistService wishlistService)
    {
        _wishlistService = wishlistService;
    }

    public async Task<IActionResult> Index()
    {
        var wishlistItems = await _wishlistService.GetWishlistAsync();

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
