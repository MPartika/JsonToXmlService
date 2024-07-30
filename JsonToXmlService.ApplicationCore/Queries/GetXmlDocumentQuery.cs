using JsonToXmlService.Domain;
using MediatR;

namespace JsonToXmlService.ApplicationCore.Queries;

public record class GetXmlDocumentQuery(int DocumentId) : IRequest<DocumentDto>;