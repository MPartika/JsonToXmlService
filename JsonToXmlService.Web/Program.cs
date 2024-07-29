using System.Text;
using System.Text.Json;
using JsonToXmlService.ApplicationCore;
using JsonToXmlService.ApplicationCore.Commands;
using JsonToXmlService.ApplicationCore.Queries;
using JsonToXmlService.Domain;
using JsonToXmlService.Web;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("System", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithProperty("AppDomain", AppDomain.CurrentDomain)
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHandlers();

var app = builder.Build();


app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "Handled {RequestPath}";

    // Emit debug-level events instead of the defaults
    options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Information;

    // Attach additional properties to the request completion event
    options.EnrichDiagnosticContext = async (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);

        var requestBody = await GetRequestBody(httpContext.Request);
        diagnosticContext.Set("RequestBody", requestBody);
    };
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/documents",  async (IMediator mediator, [FromBody] CreateDocumentCommand command) => {
    var result = await mediator.Send(command);
    return result;
})
.WithName("SaveJsonDocument")
.WithOpenApi();

app.MapGet("/documents/{id:int}", async (IMediator mediator, int id) => {
    var result = await mediator.Send(new GetXmlDocumentQuery(id));
    return new XmlResult<Document>(result);
})
.WithName("GetXMLDocument")
.WithOpenApi();

app.Run();


async Task<string> GetRequestBody(HttpRequest httpRequest)
{
    string requestBody = string.Empty;
    httpRequest.EnableBuffering();

    using var reader = new Microsoft.AspNetCore.WebUtilities.HttpRequestStreamReader(httpRequest.Body, Encoding.UTF8);
    var payload = await reader.ReadToEndAsync();
    if (!string.IsNullOrEmpty(payload))
    {
        var json = JsonSerializer.Deserialize<object>(payload);
        requestBody = $"{JsonSerializer.Serialize(json)} ";
    }

    return requestBody;
}
