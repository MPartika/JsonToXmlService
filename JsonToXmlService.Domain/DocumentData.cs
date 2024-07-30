namespace JsonToXmlService.Domain;

public record class DocumentData(string Name, string Author, string Content)
{
    public DocumentData() : this("", "", "") {}
};