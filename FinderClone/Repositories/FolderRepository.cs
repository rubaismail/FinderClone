using FinderClone.Data;
using FinderClone.Models;
using FinderClone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinderClone.Repositories;

public class FolderRepository(AppDbContext dbContext) : IFolderRepository
{
    public async Task<List<Folder>> GetFolders()
    { 
        return await dbContext.Folders.ToListAsync();
    }

    public async Task<List<Folder>> GetFolderByName(string name)
    {
        return await dbContext.Folders
            .Where(f  => EF.Functions.ILike(f.Name, $"%{name}%"))
            .ToListAsync();
    }
    
    public async Task<Folder?> GetFolderById(Guid id)
    {
        return await dbContext.Folders
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Folder> AddFolder(Folder folder)
    {
        dbContext.Folders.Add(folder);
        await dbContext.SaveChangesAsync();
        
        return folder;
    }

    public async Task<bool> UpdateFolder(Guid id, Folder folder)
    {
        var existingFolder = await dbContext.Folders.FindAsync(id);
        if (existingFolder == null) return false;
        
        existingFolder.Name = folder.Name;
        
        if(folder.ParentFolderId != null)
            existingFolder.ParentFolderId = folder.ParentFolderId;
        
        if(folder.SubFolders != null)
            existingFolder.SubFolders = folder.SubFolders;
        
        if(folder.Files != null)
            existingFolder.Files = folder.Files;
        
        await dbContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteFolder(Guid id)
    {
        var folder = await dbContext.Folders.FindAsync(id);
        
        if (folder == null) return false;
     
        dbContext.Folders.Remove(folder);
        // cascade delete is automatically on in ef core?
        await dbContext.SaveChangesAsync();
            
        return true;
    }
}