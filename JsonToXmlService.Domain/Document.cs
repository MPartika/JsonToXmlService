namespace JsonToXmlService.Domain;

public record class Document : IDocument
{
    public int Id { get; set; }
    public string[] Tags { get; init; } = [];
    public required DocumentData Data { get; init; }
}

public interface IDocument
{
    string[] Tags  { get; init; }
    DocumentData Data { get; init; }    
}
