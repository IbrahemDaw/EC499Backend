namespace Auth.Helpers;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("JWT"));

        return services;
    }
}