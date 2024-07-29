using JsonToXmlService.ApplicationCore.Queries;
using JsonToXmlService.Domain;
using MediatR;

namespace JsonToXmlService.ApplicationCore.Handlers;

public class GetXmlDocumentHandler : IRequestHandler<GetXmlDocumentQuery, Document>
{
    public async Task<Document> Handle(GetXmlDocumentQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(500);
        return new Document { Id = 10, Tags = ["important",".net"], Data = new DocumentData("Random Data", "Martin", "Lorem ipsum")};
    }
}