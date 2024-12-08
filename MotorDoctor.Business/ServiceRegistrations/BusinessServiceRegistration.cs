using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Business.Services.Implementations;
using MotorDoctor.Business.UIServices.Abstractions;
using MotorDoctor.Business.UIServices.Implementations;
using MotorDoctor.Business.Validators;
using System.Reflection;

namespace MotorDoctor.Business.ServiceRegistrations;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        AddServices(services);

        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddHttpContextAccessor();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(typeof(ProductCreateDtoValidator));

        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IAboutService, AboutService>();
        services.AddScoped<IAdvertisementService, AdvertisementService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAttendanceService, AttendanceService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<ISliderService, SliderService>();
        services.AddScoped<ISettingService, SettingService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductSizeService, ProductSizeService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<ISubscriberService, SubscriberService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<IWishlistService, WishlistService>();

        services.AddScoped<IHomeService, HomeService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<ILayoutService, LayoutService>();
    }
}
