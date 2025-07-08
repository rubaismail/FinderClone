using FinderClone.Models;
using Microsoft.EntityFrameworkCore;

namespace FinderClone.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
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
}

