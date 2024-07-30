using JsonToXmlService.ApplicationCore.Commands;
using JsonToXmlService.Domain;
using MediatR;

namespace JsonToXmlService.ApplicationCore.Handlers;

public class ChangeDocumentHandler : IRequestHandler<ChangeDocumentCommand>
{
    private readonly IDocumentRepository _repository;

    public ChangeDocumentHandler(IDocumentRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(ChangeDocumentCommand request, CancellationToken cancellationToken)
    {
        await _repository.ChangeDocumentAsync(request.DocumentId, request);
    }
}