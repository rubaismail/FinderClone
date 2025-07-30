using Application.Dtos.Folders;
using Application.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T> (AppDbContext dbContext) : IGenericRepository<T> where T : BaseEntity
{
    public async Task<List<T>> GetAll(CancellationToken cancellationToken)
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<List<T>> GetFilteredSorted(DynamicFilterSortDto filter, CancellationToken cancellationToken) // add the option to include
    {
        if (string.IsNullOrEmpty(filter.SortBy) && string.IsNullOrEmpty(filter.FilterBy))
            return await dbContext.Set<T>().ToListAsync();
        
        else if (string.IsNullOrEmpty(filter.SortBy))
        {
            var query = dbContext.Set<T>().AsQueryable()
                .ApplyDynamicFilter(filter);
            return await query.ToListAsync();
        }
        else if (string.IsNullOrEmpty(filter.FilterBy))
        {
            var query2 = dbContext.Set<T>().AsQueryable()
                .ApplyDynamicSorting(filter);
            return await query2.ToListAsync();
        }
        
        var query3 = dbContext.Set<T>().AsQueryable()
            .ApplyDynamicFilter(filter)
            .ApplyDynamicSorting(filter);

        return await query3.ToListAsync();
    }
    
    public async Task<T?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Set<T>().FindAsync([id], cancellationToken);
    }

    public async Task<T> Add(T entity, CancellationToken cancellationToken)
    {
        await dbContext.Set<T>().AddAsync(entity);
        
        return entity;
    }
    
    public async Task<bool> Update(Guid id, T entity, CancellationToken cancellationToken)
    {
        var existingT = await dbContext.Set<T>().FindAsync(id);
        if (existingT == null)
            return false;
       
        dbContext.Update(entity);

        return true;
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetById(id, cancellationToken);
        if (entity == null) return false;
        
        dbContext.Set<T>().Remove(entity);
        
        return true;
    }
}

