using Microsoft.Extensions.Localization;

namespace MotorDoctor.DataAccess.Localizers;

public class OrderLocalizer
{
    private readonly IStringLocalizer _localizer;
    public OrderLocalizer(IStringLocalizerFactory factory)
    {
        _localizer = factory.Create("Order", "MotorDoctor.Presentation");
    }

    public string GetValue(string key)
    {
        return _localizer.GetString(key);
    }
}
