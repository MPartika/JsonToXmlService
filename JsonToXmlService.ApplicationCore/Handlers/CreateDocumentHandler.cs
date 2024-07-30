using JsonToXmlService.ApplicationCore.Commands;
using JsonToXmlService.Domain;
using MediatR;

namespace JsonToXmlService.ApplicationCore.Handlers;

public class CreateDocumentHandler : IRequestHandler<CreateDocumentCommand, int>
{
    private readonly IDocumentRepository _repository;

    public CreateDocumentHandler(IDocumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        return await _repository.SaveDocumentAsync(request);        
    }
}