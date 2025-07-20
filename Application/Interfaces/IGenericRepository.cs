using Application.Dtos.Folders;

namespace Application.Interfaces;

public interface IGenericRepository<T> where T : notnull
{
    Task<List<T>> GetAll();
    Task<List<T>> GetFilteredSorted(DynamicFilterSortDto filter);
    Task<T?> GetById(Guid id);
    // Task<List<T>> GetByName(string name);
    Task<T> Add(T entity);
    Task<bool> Update(Guid id, T entity);
    Task<bool> Delete(Guid id);
}