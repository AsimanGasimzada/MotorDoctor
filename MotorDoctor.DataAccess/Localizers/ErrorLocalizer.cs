using Microsoft.Extensions.Localization;

namespace MotorDoctor.DataAccess.Localizers;

public class ErrorLocalizer
{
    private readonly IStringLocalizer _localizer;

    public ErrorLocalizer(IStringLocalizerFactory factory)
    {
        _localizer = factory.Create("Errors", "MotorDoctor.Presentation");
    }

    public string GetValue(string key)
    {
        return _localizer.GetString(key);
    }
}
