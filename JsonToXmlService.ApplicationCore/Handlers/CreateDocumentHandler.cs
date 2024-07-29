using JsonToXmlService.ApplicationCore.Commands;
using MediatR;

namespace JsonToXmlService.ApplicationCore.Handlers;

public class CreateDocumentHandler : IRequestHandler<CreateDocumentCommand, int>
{
    public async Task<int> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(500, cancellationToken);
        Random rnd = new();
        return rnd.Next(1, 100);
    }
}