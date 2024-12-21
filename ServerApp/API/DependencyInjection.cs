using Application;
using Persistence;

namespace API;

public static class DependencyInjection
{
    public static void AddProgramDependencies(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors();
        
        services.AddApplication();
        services.AddPersistence(configuration);
    }

    private static void AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
}
