using CoffeeApplication.BusinessLogic.Service;
using CoffeeApplication.Common;
using CoffeeApplication.Data;
using CoffeeApplication.Data.DataStore;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CoffeeApplication.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        // use two-stage initialization for serilog
        // configure first in a try/catch block to ensure any configuration issues are appropriately logged
        // https://github.com/serilog/serilog-aspnetcore

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            Log.Information("Starting application");

            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);

            var app = builder.Build();

            ConfigurePipeline(app);

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void ConfigurePipeline(WebApplication app)
    {

        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.Configure<AppSettings>(builder.Configuration);
        var appSettings = builder.Configuration.Get<AppSettings>();
        ConfigureData(builder.Services, appSettings?.ConnectionStrings?.CoffeeApplicationConnection);
        ConfigureServices(builder.Services);
        ConfigureHsts(builder.Services);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    private static void ConfigureData(IServiceCollection services, string? coffeeApplicationConnection)
    {
        if (coffeeApplicationConnection == null)
        {
            throw new ArgumentNullException(nameof(coffeeApplicationConnection));
        }

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(coffeeApplicationConnection);
        });

        services.AddScoped<IDataStore, DataStore>();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<OrderService>();
    }

    private static void ConfigureHsts(IServiceCollection services)
    {
        // AddHsts and UseHttpsRedirection is used to require https for all requests and redirect all http requests to https:
        // https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-7.0

        // 2 years is the recommended max-age value for stable applications:
        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Strict-Transport-Security
        // https://hstspreload.org/

        services.AddHsts(options =>
        {
            options.Preload = true;
            options.IncludeSubDomains = true;
            options.MaxAge = TimeSpan.FromSeconds(63072000);
        });
    }
}
