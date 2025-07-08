using FinderClone.Models;

namespace FinderClone.Repositories.Interfaces;

public interface IFolderRepository
{
    Task <List<Folder>> GetFolders();
    Task<List<Folder>> GetFolderByName(string name);
    Task<Folder?> GetFolderById(Guid id);
    Task<Folder> AddFolder(Folder folder);
    Task<bool> UpdateFolder(Guid id, Folder folder);
    Task <bool> DeleteFolder(Guid id);
}