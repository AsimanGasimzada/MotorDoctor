using Microsoft.Extensions.DependencyInjection;
using MotorDoctor.Business.Services.Abstractions;
using MotorDoctor.Business.Services.Implementations;
using MotorDoctor.DataAccess.Repositories.Abstractions;
using MotorDoctor.DataAccess.Repositories.Implementations;
using System.Reflection;

namespace MotorDoctor.Business.ServiceRegistrations;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());


        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<ISliderService, SliderService>();
        services.AddScoped<ISettingService, SettingService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }

}
