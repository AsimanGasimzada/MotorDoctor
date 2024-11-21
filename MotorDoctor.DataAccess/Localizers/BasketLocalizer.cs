using Microsoft.Extensions.Localization;

namespace MotorDoctor.DataAccess.Localizers;

public class BasketLocalizer
{
    private readonly IStringLocalizer _localizer;

    public BasketLocalizer(IStringLocalizerFactory factory)
    {
        _localizer = factory.Create("Basket", "MotorDoctor.Presentation");
    }

    public string GetValue(string key)
    {
        return _localizer.GetString(key);
    }
}
