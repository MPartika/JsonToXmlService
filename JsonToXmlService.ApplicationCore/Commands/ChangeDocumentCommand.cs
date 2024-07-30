using JsonToXmlService.Domain;
using MediatR;

namespace JsonToXmlService.ApplicationCore.Commands;

public record class ChangeDocumentCommand : IDocumentDto, IRequest
{
    public int Id { get; set; }
    public string[] Tags { get; init; } = [];
    public required DocumentData Data { get; init; }
}