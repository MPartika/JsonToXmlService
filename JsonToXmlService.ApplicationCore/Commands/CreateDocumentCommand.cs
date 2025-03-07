using JsonToXmlService.Domain;
using MediatR;

namespace JsonToXmlService.ApplicationCore.Commands;

public record class CreateDocumentCommand : IDocumentDto, IRequest<int>
{
    public string[] Tags { get; init; } = [];
    public required DocumentData Data { get; init; }
}