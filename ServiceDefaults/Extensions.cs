// Ignore Spelling: app

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Microsoft.Extensions.Hosting;

public static class Extensions
{
    // Configure services with database
    public static IHostApplicationBuilder AddServiceDefaults<T>(this IHostApplicationBuilder builder) where T : DbContext
    {
        builder.ConfigureDbContext<T>();
        return builder;
    }

    // Logging configuration
    public static void AddLogging(this IHostBuilder builder)
    {
        builder.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
        });
    }

    public static IHostApplicationBuilder ConfigureDbContext<T>(this IHostApplicationBuilder builder) where T : DbContext
    {
        builder.Services.AddDbContext<T>(c =>
        {
            c.UseMySQL(builder.Configuration.GetConnectionString("AppDb")!);
            
        });
        return builder;
    }

    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        return app;
    }
}