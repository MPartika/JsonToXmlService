#pragma warning disable CS1573 
#pragma warning disable CS1591 
using JsonToXmlService.ApplicationCore.Commands;
using JsonToXmlService.ApplicationCore.Queries;
using JsonToXmlService.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JsonToXmlService.Web;

public static class ConverterEndpoints
{
    public static WebApplication MapConverterEndpoints(this WebApplication app)
    {
        app.MapPost("/documents", SaveJsonDocument)
        .WithName("SaveJsonDocument")
        .WithOpenApi();

        app.MapPut("/documents/{id:int}", ChangeJsonDocument)
        .WithName("ChangeJsonDocument")
        .WithOpenApi();

        app.MapGet("/documents/{id:int}", GetXmlDocument)
        .WithName("GetXMLDocument")
        .WithOpenApi();
        return app;
    }

    /// <summary>
    /// Save document to db 
    /// </summary>
    /// <remarks>
    /// {
    ///   "tags": [
    ///     ".net", "important"
    ///   ],
    ///   "data": {
    ///     "name": "Martin's document",
    ///     "author": "Martin",
    ///     "content": "Lorem Ipsum"
    ///   }
    /// }
    /// </remarks>
    public static async Task<IResult> SaveJsonDocument(IMediator mediator, [FromBody] CreateDocumentCommand command)
    {
        var result = await mediator.Send(command);
        return Results.Ok(result);
    }

    /// <summary>
    /// Get saved document as xml
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="id" example="1"></param>
    /// <returns></returns>
    public static async Task<IResult> GetXmlDocument(IMediator mediator, int id)
    {
        var result = await mediator.Send(new GetXmlDocumentQuery(id));
        return new XmlResult<DocumentDto>(result);
    }

    /// <summary>
    /// Changing stored documents
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="id" example="1"></param>
    /// <param name="command"></param>
    /// <remarks>
    /// {
    ///   "tags": [
    ///     ".net", "important"
    ///   ],
    ///   "data": {
    ///     "name": "Martin's document",
    ///     "author": "Martin",
    ///     "content": "Lorem Ipsum"
    ///   }
    /// }
    /// </remarks>
    public static async Task<IResult> ChangeJsonDocument(IMediator mediator, int id, [FromBody] ChangeDocumentCommand command)
    {
        command.DocumentId = id;
        await mediator.Send(command);
        return Results.Ok();
    }
}