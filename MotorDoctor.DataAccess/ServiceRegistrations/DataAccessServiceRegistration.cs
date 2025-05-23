﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotorDoctor.DataAccess.Contexts;
using MotorDoctor.DataAccess.DataInitializers;
using MotorDoctor.DataAccess.Helpers;
using MotorDoctor.DataAccess.Interceptors;
using MotorDoctor.DataAccess.Localizers;
using MotorDoctor.DataAccess.Repositories.Abstractions;
using MotorDoctor.DataAccess.Repositories.Abstractions.Generic;
using MotorDoctor.DataAccess.Repositories.Implementations;

namespace MotorDoctor.DataAccess.ServiceRegistrations;

public static class DataAccessServiceRegistration
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddScoped<BaseEntityInterceptor>();
        services.AddScoped<DbContextInitalizer>();

        services.AddMemoryCache();

        _addRepositories(services);
        _addIdentity(services);
        _addLocalizers(services);

        return services;
    }

    private static void _addRepositories(IServiceCollection services)
    {
        services.AddScoped<IAboutRepository, AboutRepository>();
        services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
        services.AddScoped<IAttedanceRepository, AttedanceRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IBasketItemRepository, BasketItemRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IDensityRepository, DensityRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<ISliderRepository, SliderRepository>();
        services.AddScoped<ISettingRepository, SettingRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
        services.AddScoped<ISubscriberRepository, SubscriberRepository>();
        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IWishlistItemRepository, WishlistItemRepository>();
    }

    private static void _addLocalizers(IServiceCollection services)
    {
        services.AddSingleton<AboutLocalizer>();
        services.AddSingleton<AccountLocalizer>();
        services.AddSingleton<BasketLocalizer>();
        services.AddSingleton<BranchLocalizer>();
        services.AddSingleton<ContactLocalizer>();
        services.AddSingleton<ErrorLocalizer>();
        services.AddSingleton<HomeLocalizer>();
        services.AddSingleton<LayoutLocalizer>();
        services.AddSingleton<OrderLocalizer>();
        services.AddSingleton<ShopLocalizer>();
        services.AddSingleton<WishlistLocalizer>();
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
