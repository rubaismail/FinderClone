using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IGenericRepository<Folder> FoldersRepo => new GenericRepository<Folder>(_dbContext);

    public IGenericRepository<StoredFile> FilesRepo => new GenericRepository<StoredFile>(_dbContext);
    
    public IGenericRepository<User> UsersRepo => new GenericRepository<User>(_dbContext);

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}