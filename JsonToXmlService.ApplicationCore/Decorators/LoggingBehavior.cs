using System.Text.Json;
using MediatR;
using Serilog;

namespace JsonToXmlService.ApplicationCore.Decorators;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        return await LoggingBaseBehavior<TRequest, TResponse>.Handle(request, next);
    }
}


public class LoggingResponseBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        return await LoggingBaseBehavior<TRequest, TResponse>.Handle(request, next);
    }
}

internal class LoggingBaseBehavior<TRequest, TResponse>
{
    public static async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
    {
        Log.Information(
            $"Command Executed: {typeof(TRequest)}: {JsonSerializer.Serialize(request)}"
        );

        try
        {
            var response = await next();
            Log.Information($"Command {typeof(TRequest)} executed successfully.");
            return response;
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"Command {typeof(TRequest)} failed during execution.");
            throw;
        }
    }
}