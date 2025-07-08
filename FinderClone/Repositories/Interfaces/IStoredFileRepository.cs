using FinderClone.Dtos;
using FinderClone.Models;

namespace FinderClone.Repositories.Interfaces;

public interface IStoredFileRepository
{
    Task <List<StoredFile>> GetAllFiles();
    Task<List<StoredFile>> GetFileByName(string name);
    Task<StoredFile?> GetFileById(Guid id);
    Task<StoredFile> AddFile(StoredFile file);
    Task<bool> UpdateFile(Guid id, StoredFile file);
    Task <bool> DeleteFile(Guid id);
}