using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Entities.Audit;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    private readonly IUserContextService _userContext;

    public AppDbContext(DbContextOptions<AppDbContext> options, IUserContextService userContext) : base(options)
    {
        _userContext = userContext;
    }

    public DbSet<StoredFile> Files { get; set; }
    public DbSet<Folder> Folders { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Folder>()
            .HasOne(a => a.ParentFolder)
            .WithMany(f => f.SubFolders)
            .HasForeignKey(a => a.ParentFolderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Folder>()
            .Property(f => f.Name).IsRequired().HasMaxLength(30);
        
        modelBuilder.Entity<StoredFile>()
            .Property(sf => sf.Name).IsRequired().HasMaxLength(30);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<IAuditable>();

        var userId = _userContext.GetCurrentUserId(); 

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedOn = DateTime.UtcNow;
                entry.Entity.CreatedById = userId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedOn = DateTime.UtcNow;
                entry.Entity.UpdatedById = userId;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}