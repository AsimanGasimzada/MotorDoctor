using Microsoft.Extensions.Localization;

namespace MotorDoctor.DataAccess.Localizers;

public class BranchLocalizer
{
    private readonly IStringLocalizer _localizer;
    public BranchLocalizer(IStringLocalizerFactory factory)
    {
        _localizer = factory.Create("Branch", "MotorDoctor.Presentation");
    }

    public string GetValue(string key)
    {
        return _localizer.GetString(key);
    }
}
