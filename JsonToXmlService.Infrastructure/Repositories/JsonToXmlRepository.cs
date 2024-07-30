using JsonToXmlService.Domain;
using JsonToXmlService.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace JsonToXmlService.Infrastructure.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly DocumentDbContext _dbContext;

    public DocumentRepository(DocumentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveDocumentAsync(IDocumentDto documentDto)
    {
        var document = documentDto.ToDocument();
        _dbContext.Add(document);
        await _dbContext.SaveChangesAsync();
        return document.Id;
    }

    public async Task ChangeDocumentAsync(int documentId, IDocumentDto documentDto)
    {
        var document = await _dbContext.Documents.SingleOrDefaultAsync(x => x.Id == documentId);
        if (document == null)
            return;
        document.Tags = string.Join(",", documentDto.Tags);
        document.Name = documentDto.Data.Name;
        document.Author = documentDto.Data.Author;
        document.Content = documentDto.Data.Content;

        await _dbContext.SaveChangesAsync();        
    }

    public async Task<DocumentDto?> GetDocumentAsync(int documentId)
    {
        var document = await _dbContext.Documents.SingleOrDefaultAsync(x => x.Id == documentId);
        if (document == null)
            return null;
        
        return document.ToDocumentDto();  
    }
}