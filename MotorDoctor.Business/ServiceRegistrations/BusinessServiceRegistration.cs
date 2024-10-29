using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MotorDoctor.Business.ServiceRegistrations;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }

}
