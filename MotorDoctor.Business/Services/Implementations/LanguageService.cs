using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Services.Implementations;

internal class LanguageService : ILanguageService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public LanguageService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public void SelectCulture(string culture)
    {
        Console.WriteLine("Selected lang " + culture);

        if (culture != "az" && culture != "en" && culture != "ru")
            culture = "az";

        Languages selectedLanguage = Languages.Azerbaijan;

        if (culture == "en")
            selectedLanguage = Languages.English;
        else if (culture == "ru")
            selectedLanguage = Languages.Russian;

        if (!string.IsNullOrEmpty(culture))
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTime.UtcNow.AddYears(1) }
                );

            _contextAccessor.HttpContext?.Response.Cookies.Append("SelectedLanguage", culture);
            Constants.SelectedLanguage = selectedLanguage;
        }
    }
}
