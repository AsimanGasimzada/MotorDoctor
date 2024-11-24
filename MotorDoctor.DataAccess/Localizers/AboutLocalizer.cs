using Microsoft.Extensions.Localization;

namespace MotorDoctor.DataAccess.Localizers;

public class AboutLocalizer
{
    private readonly IStringLocalizer _localizer;
    public AboutLocalizer(IStringLocalizerFactory factory)
    {
        _localizer = factory.Create("About", "MotorDoctor.Presentation");
    }

    public string GetValue(string key)
    {
        return _localizer.GetString(key);
    }
}
