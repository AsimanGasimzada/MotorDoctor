using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using MotorDoctor.Business.ServiceRegistrations;
using MotorDoctor.DataAccess.ServiceRegistrations;
using MotorDoctor.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();


builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddBusinessServices();

var app = builder.Build();


var supportedCultures = new[] { "az", "en", "ru" };

var localizationOptions = new RequestLocalizationOptions()
                                                .SetDefaultCulture(supportedCultures[0])
                                                .AddSupportedCultures(supportedCultures)
                                                .AddSupportedUICultures(supportedCultures);


localizationOptions.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
{
    var cultureCookie = context.Request.Cookies[".AspNetCore.Culture"];

    if (!string.IsNullOrEmpty(cultureCookie))
    {
        return new ProviderCultureResult(cultureCookie);
    }

    return new ProviderCultureResult("az", "az");
}));


if (!app.Environment.IsDevelopment())
    app.UseMiddleware<GlobalExceptionHandler>();



app.UseRequestLocalization(localizationOptions);

app.RenderSelectedLanguage();

await app.InitDatabaseAsync();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
