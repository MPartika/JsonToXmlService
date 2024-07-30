namespace JsonToXmlService.Domain;

public record class DocumentDto : IDocumentDto
{
    public int Id { get; set; }
    public string[] Tags { get; init; } = [];
    public required DocumentData Data { get; init; }
}

public interface IDocumentDto
{
    string[] Tags  { get; init; }
    DocumentData Data { get; init; }    
}
