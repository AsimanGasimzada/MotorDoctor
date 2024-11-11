using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Business.Services.Implementations;
using System.Reflection;

namespace MotorDoctor.Business.ServiceRegistrations;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());


        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAttendanceService, AttendanceService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<ISliderService, SliderService>();
        services.AddScoped<ISettingService, SettingService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductSizeService, ProductSizeService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<ISubscriberService, SubscriberService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<IOrderService, OrderService>();


        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddHttpContextAccessor();

        return services;
    }

}
