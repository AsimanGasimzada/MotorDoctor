using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotorDoctor.DataAccess.Contexts;
using MotorDoctor.DataAccess.DataInitializers;
using MotorDoctor.DataAccess.Helpers;
using MotorDoctor.DataAccess.Interceptors;
using MotorDoctor.DataAccess.Repositories.Abstractions;
using MotorDoctor.DataAccess.Repositories.Implementations;
using MotorDoctor.DataAccess.Localizers;

namespace MotorDoctor.DataAccess.ServiceRegistrations;

public static class DataAccessServiceRegistration
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
        _addRepositories(services);
        services.AddScoped<BaseEntityInterceptor>();
        _addIdentity(services);

        services.AddScoped<DbContextInitalizer>();

        return services;
    }

    private static void _addRepositories(IServiceCollection services)
    {
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IBasketItemRepository, BasketItemRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISliderRepository, SliderRepository>();
        services.AddScoped<ISettingRepository, SettingRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
        services.AddScoped<IAttedanceRepository, AttedanceRepository>();
        services.AddScoped<ISubscriberRepository, SubscriberRepository>();

        services.AddSingleton<ErrorLocalizer>();
    }

    private static void _addIdentity(IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
            options.Lockout.AllowedForNewUsers = false;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 3;

        }).AddEntityFrameworkStores<AppDbContext>()
          .AddDefaultTokenProviders()
          .AddErrorDescriber<CustomIdentityErrorDescriber>();
    }
}
