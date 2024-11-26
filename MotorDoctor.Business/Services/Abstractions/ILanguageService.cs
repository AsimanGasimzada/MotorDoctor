using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Services.Abstractions;

public interface ILanguageService
{
    void SelectCulture(string culture);
    Languages RenderSelectedLanguage();
}
