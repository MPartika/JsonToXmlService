using JsonToXmlService.Infrastructure.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace JsonToXmlService.Infrastructure;

public class JsonToXmlDbContext : DbContext
{
    private readonly string _dbPath = Path.Join(GetPath, "Random.db");

    public DbSet<Document> Documents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite($"Data Source={_dbPath}")
           .LogTo(Log.Information, LogLevel.Information);

    public override int SaveChanges()
    {
        var entities = ChangeTracker
            .Entries()
            .Where(entity =>
                entity.Entity is IDbEntity
                && (entity.State == EntityState.Added || entity.State == EntityState.Modified)
            );
        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
                ((IDbEntity)entity.Entity).Created = DateTime.UtcNow;
            ((IDbEntity)entity.Entity).Updated = DateTime.UtcNow;
        }
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entities = ChangeTracker
            .Entries()
            .Where(entity =>
                entity.Entity is IDbEntity
                && (entity.State == EntityState.Added || entity.State == EntityState.Modified)
            );
        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
                ((IDbEntity)entity.Entity).Created = DateTime.UtcNow;
            ((IDbEntity)entity.Entity).Updated = DateTime.UtcNow;
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    private static string GetPath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
}