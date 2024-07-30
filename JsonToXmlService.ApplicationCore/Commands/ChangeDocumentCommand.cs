using JsonToXmlService.Domain;
using MediatR;

namespace JsonToXmlService.ApplicationCore.Commands;

public record class ChangeDocumentCommand : IDocumentDto, IRequest
{
    public string[] Tags { get; init; } = [];
    public required DocumentData Data { get; init; }
}