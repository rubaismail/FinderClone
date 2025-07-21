using Application.Dtos.Folders;

namespace Application.Interfaces;

public interface IGenericRepository<T> where T : notnull
{
    Task<List<T>> GetAll(CancellationToken cancellationToken);
    Task<List<T>> GetFilteredSorted(DynamicFilterSortDto filter, CancellationToken cancellationToken);
    Task<T?> GetById(Guid id, CancellationToken cancellationToken);
    Task<T> Add(T entity, CancellationToken cancellationToken);
    Task<bool> Update(Guid id, T entity, CancellationToken cancellationToken);
    Task<bool> Delete(Guid id, CancellationToken cancellationToken);
}