namespace JsonToXmlService.Domain;

public interface IJsonToXmlRepository : IDependency
{
    Task<int> SaveJsonAsync(IDocumentDto documentDto);
    Task<DocumentDto?> GetJson(int id);
}