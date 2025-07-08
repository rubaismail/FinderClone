using FinderClone.Data;
using FinderClone.Models;
using FinderClone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinderClone.Repositories;

public class StoredFileRepository(AppDbContext dbContext) : IStoredFileRepository
{
    public async Task<List<StoredFile>> GetAllFiles()
    {
        return await dbContext.Files.ToListAsync();
    }

    public async Task<List<StoredFile>> GetFileByName(string name)
    {
        return await dbContext.Files
            .Where(f  => EF.Functions.ILike(f.Name, $"%{name}%"))
            .ToListAsync();
    }

    public async Task<StoredFile?> GetFileById(Guid id)
    {
        return await dbContext.Files
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<StoredFile> AddFile(StoredFile file)
    {
        await dbContext.Files.AddAsync(file);
        await dbContext.SaveChangesAsync();
        return file;
    }

    public async Task<bool> UpdateFile(Guid id, StoredFile file)
    {
        var currentFile = await dbContext.Files.FindAsync(id);
        
        if (currentFile == null)
            return false;
        else
        {
            currentFile.Name = file.Name;
            currentFile.ParentFolderId = file.ParentFolderId;
            return true;
        }
    }

    public async Task<bool> DeleteFile(Guid id)
    {
        var file = await dbContext.Files.FindAsync(id);
        
        if (file == null)
            return false;
        else
        {
            dbContext.Files.Remove(file);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}