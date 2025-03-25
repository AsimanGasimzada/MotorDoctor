using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Services.Implementations;

internal class LanguageService : ILanguageService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private const string COOKIE_KEY = "SelectedLanguage";
    public Languages SelectedLanguage
    {
        get
        {
            return _getEnumValue(GetSelectedCulture());
        }
    }

    public LanguageService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Languages RenderSelectedLanguage()
    {
        string culture = GetSelectedCulture();

        var language = _getEnumValue(culture);

        Constants.SelectedLanguage = language;

        return language;

    }

    public void SelectCulture(string culture)
    {
        Console.WriteLine("Selected lang " + culture);

        if (culture != "az" && culture != "en" && culture != "ru")
            culture = "az";

        Languages selectedLanguage = _getEnumValue(culture);

        if (!string.IsNullOrEmpty(culture))
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTime.UtcNow.AddYears(1) }
                );

            _contextAccessor.HttpContext?.Response.Cookies.Append(COOKIE_KEY, culture);
            Constants.SelectedLanguage = selectedLanguage;
        }
    }

    private static Languages _getEnumValue(string culture)
    {
        Languages selectedLanguage = Languages.Azerbaijan;

        if (culture == "en")
            selectedLanguage = Languages.English;
        else if (culture == "ru")
            selectedLanguage = Languages.Russian;

        return selectedLanguage;
    }

    public string GetSelectedCulture()
    {
        string? culture = _contextAccessor.HttpContext?.Request.Cookies[COOKIE_KEY];

        if (string.IsNullOrWhiteSpace(culture))
        {
            culture = "az";
            SelectCulture(culture);
        }

        return culture;
    }
}
