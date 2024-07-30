using JsonToXmlService.Domain;
using JsonToXmlService.Infrastructure;
using JsonToXmlService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JsonToXmlService.Test;

public class Tests
{
    private DocumentDbContext _dbContext;
    private DocumentDto _document = new DocumentDto { Tags=[".net", "test"], Data = new DocumentData("TestName", "TestAuthor", "TestContent")};

    [SetUp]
    public void Setup()
    {
        _dbContext = new DocumentDbContext("UnitTestDb.db");
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.Migrate();
    }

    [TearDown]
    public void Cleanup()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    [Test]
    public async Task ShouldCreateDocument()
    {
        var repository = new DocumentRepository(_dbContext);
        var id = await repository.SaveDocumentAsync(_document);
        Assert.Greater(id, 0);
    }

    [Test]
    public async Task ShouldUpdateDocument()
    {
        var repository = new DocumentRepository(_dbContext);
        var id = await repository.SaveDocumentAsync(_document);
        Assert.Greater(id, 0);
        var updatedVersion = _document with { Data = new DocumentData("New Name", "New Author", "New Content")};
        await repository.ChangeDocumentAsync(id, updatedVersion);
        Assert.Pass();
    }

    [Test]
    public async Task ShouldGetDocument()
    {
        var repository = new DocumentRepository(_dbContext);
        var id = await repository.SaveDocumentAsync(_document);
        Assert.Greater(id, 0);
        var updatedVersion = _document with { Id = id};
        var document = await repository.GetDocumentAsync(id);
        Assert.NotNull(document);
        Assert.That(updatedVersion.Data, Is.EqualTo(document.Data));
        Assert.That(string.Join(",", updatedVersion.Tags), Is.EqualTo(string.Join(",", document.Tags)));
    }
}