using Domain.Entities;

namespace Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Folder> FoldersRepo { get; }
    IGenericRepository<StoredFile> FilesRepo { get; }
    IGenericRepository<User> UsersRepo { get; }
    public Task SaveChangesAsync();
}