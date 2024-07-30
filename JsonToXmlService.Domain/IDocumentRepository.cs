namespace JsonToXmlService.Domain;

public interface IDocumentRepository : IDependency
{
    Task<int> SaveDocumentAsync(IDocumentDto documentDto);
    Task<DocumentDto?> GetJson(int id);
    Task ChangeDocumentAsync(int documentId, IDocumentDto documentDto);
}