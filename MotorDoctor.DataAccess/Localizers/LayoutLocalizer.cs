using Microsoft.Extensions.Localization;

namespace MotorDoctor.DataAccess.Localizers;

public class LayoutLocalizer
{
    private readonly IStringLocalizer _localizer;

    public LayoutLocalizer(IStringLocalizerFactory factory)
    {
        _localizer = factory.Create("Layout", "MotorDoctor.Presentation");
    }

    public string GetValue(string key)
    {
        return _localizer.GetString(key);
    }
}
