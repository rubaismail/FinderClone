namespace FinderClone.Repositories.Interfaces;

public interface IUowRepository
{
    Task<bool> UpdateParentFolder(Guid FolderOrFileId, Guid newParentId);
    
}