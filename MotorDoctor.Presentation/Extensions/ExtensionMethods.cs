using MotorDoctor.DataAccess.DataInitializers;
using System.Text.RegularExpressions;

namespace MotorDoctor.Presentation.Extensions;

public static class ExtensionMethods
{
    public static async Task InitDatabaseAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var initializer = scope.ServiceProvider.GetRequiredService<DbContextInitalizer>();
            await initializer.InitDatabaseAsync();
        }
    }


    public static string StripHtmlTags(this string input)
    {
        return Regex.Replace(input, "<.*?>", string.Empty);
    }

    public static string GetReturnUrl(this HttpRequest Request)
    {
        string? returnUrl = Request.Headers["Referer"];

        if (string.IsNullOrEmpty(returnUrl))
            returnUrl = "/";

        return returnUrl;
    }

    public static decimal CalculateDiscountedPrice(this decimal price, decimal discount)
    {
        return Math.Round((price - price * discount / 100), 2);
    }
}
