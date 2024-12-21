using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<CarService>();
        services.AddTransient<EmployeeService>();
        services.AddTransient<AccessCheckService>();
        return services;
    }
}