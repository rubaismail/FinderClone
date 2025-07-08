using FinderClone.Data;
using FinderClone.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinderClone.Repositories;

public class UowRepository (AppDbContext dbContext) : IUowRepository
{
    public async Task<bool> UpdateParentFolder(Guid FolderOrFileId, Guid newParentId)
    {
        var folder = await dbContext.Folders.FindAsync(FolderOrFileId);

        if (folder == null)
        {
            var file = await dbContext.Files.FindAsync(FolderOrFileId);
            if (file != null)
            {
                file.ParentFolderId = newParentId;
                dbContext.SaveChanges();
                return true;
            }
            else return false;
        }
        folder.ParentFolderId = newParentId;
        dbContext.SaveChanges();
        return true;
    }
}
