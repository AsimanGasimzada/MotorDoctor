using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MotorDoctor.Presentation.Views.Home
{
    public class LangModel : PageModel
    {
        public void OnGet()
        {
            //string? culture = Request.Query["lang"];
            //Console.WriteLine("Selected lang " + culture);

            //if (!string.IsNullOrEmpty(culture))
            //{
            //    Response.Cookies.Append(
            //        CookieRequestCultureProvider.DefaultCookieName,
            //        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            //        new CookieOptions { Expires = DateTime.UtcNow.AddYears(1) }
            //        );
            //}

            //string returnUrl = Request.Headers["Referer"].ToString() ?? "/";
            //Response.Redirect(returnUrl);
        }
    }
}
