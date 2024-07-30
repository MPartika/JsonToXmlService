using JsonToXmlService.Domain;
using JsonToXmlService.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace JsonToXmlService.Infrastructure.Repositories;

public class JsonToXmlRepository : IJsonToXmlRepository
{
    private readonly JsonToXmlDbContext _dbContext;

    public JsonToXmlRepository(JsonToXmlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveJsonAsync(IDocumentDto documentDto)
    {
        var document = documentDto.ToDocument();
        _dbContext.Add(document);
        await _dbContext.SaveChangesAsync();
        return document.Id;
    }

    public async Task<DocumentDto?> GetJson(int id)
    {
        var document = await _dbContext.Documents.SingleOrDefaultAsync(x => x.Id == id);
        if (document == null)
            return null;
        
        return document.ToDocumentDto();  
    }
}