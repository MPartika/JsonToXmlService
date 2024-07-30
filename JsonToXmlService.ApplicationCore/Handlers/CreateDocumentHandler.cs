using JsonToXmlService.ApplicationCore.Commands;
using JsonToXmlService.Domain;
using MediatR;

namespace JsonToXmlService.ApplicationCore.Handlers;

public class CreateDocumentHandler : IRequestHandler<CreateDocumentCommand, int>
{
    private readonly IJsonToXmlRepository _repository;

    public CreateDocumentHandler(IJsonToXmlRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        return await _repository.SaveJsonAsync(request);        
    }
}