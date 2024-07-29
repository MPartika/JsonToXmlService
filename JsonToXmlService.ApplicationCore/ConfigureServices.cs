using System.Reflection;
using FluentValidation;
using JsonToXmlService.ApplicationCore.Decorators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JsonToXmlService.ApplicationCore;

public static class ConfigureServices
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingResponseBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationResponseBehavior<,>));
        return services;
    }
}
