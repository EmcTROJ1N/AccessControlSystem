using Domain.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = "Server=KOMPUTER;Database=AccessControlSystem;Trusted_Connection=True;TrustServerCertificate=True";
        services.AddDbContext<DbAccessSystemContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddRepositories();
        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<ICarStore, CarRepository>()
            .AddTransient<IEmployeeStore, EmployeeRepository>()
            .AddTransient<IAccessCheckStore, AccessCheckRepository>();
    }
}
