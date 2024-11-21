using Microsoft.Extensions.Localization;

namespace MotorDoctor.DataAccess.Localizers;

public class AccountLocalizer
{
    private readonly IStringLocalizer _localizer;
    public AccountLocalizer(IStringLocalizerFactory factory)
    {
        _localizer = factory.Create("Account", "MotorDoctor.Presentation");
    }

    public string GetValue(string key)
    {
        return _localizer.GetString(key);
    }
}
