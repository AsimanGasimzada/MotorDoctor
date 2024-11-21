using Microsoft.Extensions.Localization;

namespace MotorDoctor.DataAccess.Localizers;

public class ShopLocalizer
{
    private readonly IStringLocalizer _localizer;
    public ShopLocalizer(IStringLocalizerFactory factory)
    {
        _localizer = factory.Create("Shop", "MotorDoctor.Presentation");
    }

    public string GetValue(string key)
    {
        return _localizer.GetString(key);
    }
}
