using Fluorite.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AAM.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddAutoMapper(config =>
        {
            config.ConstructServicesUsing(t => services.BuildServiceProvider().GetRequiredService(t));
            config.AddProfile<AppProfile>();
        });
        services.AddStrainer(
            configuration: configuration.GetSection("Strainer"),
            moduleTypes: new[] { typeof(ApplicationStrainerModule) }
        );
    }
}
