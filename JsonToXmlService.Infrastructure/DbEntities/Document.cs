
namespace JsonToXmlService.Infrastructure.DbEntities;

public class Document : IDbEntity
{
    public int Id { get; set; }
    public string Tags {get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Author {get; set;} = string.Empty; 
    public string Content {get; set;} = string.Empty;

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}