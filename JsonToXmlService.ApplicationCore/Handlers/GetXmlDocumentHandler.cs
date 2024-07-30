using JsonToXmlService.ApplicationCore.Queries;
using JsonToXmlService.Domain;
using MediatR;

namespace JsonToXmlService.ApplicationCore.Handlers;

public class GetXmlDocumentHandler : IRequestHandler<GetXmlDocumentQuery, DocumentDto>
{
    private readonly IJsonToXmlRepository _repository;

    public GetXmlDocumentHandler(IJsonToXmlRepository repository)
    {
        _repository = repository;
    }

    public async Task<DocumentDto> Handle(GetXmlDocumentQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetJson(request.DocumentId) ?? new DocumentDto{ Data = new DocumentData()};
    }
}