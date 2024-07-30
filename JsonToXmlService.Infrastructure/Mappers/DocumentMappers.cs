using JsonToXmlService.Domain;
using JsonToXmlService.Infrastructure.DbEntities;

namespace JsonToXmlService.Infrastructure.Mappers;

internal static class DocumentMappers
{
    public static Document ToDocument(this IDocumentDto documentDto)
    {
        return new Document
        {   
            Tags = string.Join(",", documentDto.Tags),
            Name = documentDto.Data.Name,
            Author = documentDto.Data.Author,
            Content= documentDto.Data.Content,
        };
    }

    public static DocumentDto ToDocumentDto(this Document document)
    {
        return new DocumentDto
        {
            Id = document.Id,
            Tags = document.Tags.Split(","),
            Data = new DocumentData(document.Name, document.Author, document.Content)
        };
    }
    
}