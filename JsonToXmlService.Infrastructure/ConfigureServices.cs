using System.Reflection;
using JsonToXmlService.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace JsonToXmlService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var assemblyServices = Assembly.GetExecutingAssembly();
        var typesServices = assemblyServices
            .GetTypes()
            .Where(type => type.GetInterfaces().Contains(typeof(IDependency)))
            .ToList();
        typesServices.ForEach(type =>
        {
            var serviceType = type.GetInterfaces().FirstOrDefault(i => i != typeof(IDependency));
            if (serviceType != null)
                services.AddTransient(serviceType, type);
        });

        return services;
    }

    public static IServiceCollection AddDatabaseContext(this IServiceCollection services)
    {
        services.AddDbContext<JsonToXmlDbContext>(ServiceLifetime.Scoped);
        return services;
    }
}