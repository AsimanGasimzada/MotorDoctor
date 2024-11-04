﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotorDoctor.DataAccess.Contexts;
using MotorDoctor.DataAccess.Interceptors;
using MotorDoctor.DataAccess.Repositories.Abstractions;
using MotorDoctor.DataAccess.Repositories.Implementations;

namespace MotorDoctor.DataAccess.ServiceRegistrations;

public static class DataAccessServiceRegistration
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
        AddRepositories(services);
        services.AddScoped<BaseEntityInterceptor>();

        return services;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISliderRepository, SliderRepository>();
        services.AddScoped<ISettingRepository, SettingRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}