using Microsoft.Extensions.Localization;

namespace MotorDoctor.DataAccess.Localizers;

public class WishlistLocalizer
{
    private readonly IStringLocalizer _localizer;
    public WishlistLocalizer(IStringLocalizerFactory factory)
    {
        _localizer = factory.Create("Wishlist", "MotorDoctor.Presentation");
    }

    public string GetValue(string key)
    {
        return _localizer.GetString(key);
    }
}
