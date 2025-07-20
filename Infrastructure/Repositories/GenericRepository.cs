using Application.Dtos.Folders;
using Application.Extensions;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T> (AppDbContext dbContext) : IGenericRepository<T> where T : BaseEntity
{
    public async Task<List<T>> GetAll()
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<List<T>> GetFilteredSorted(DynamicFilterSortDto filter) // add the option to include
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
    
    public async Task<T?> GetById(Guid id)
    {
        return await dbContext.Set<T>()
            .FirstOrDefaultAsync(t => t.Id == id);
    }
    
    /*
     public async Task<List<T>> GetByName(string name)
    
    {
        return await dbContext.Set<T>()
            .Where(t => EF.Functions.ILike(t.Name, $"%{name}%")).ToListAsync();
    }
    */
    
    public async Task<T> Add(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
        
        return entity;
    }
    
    public async Task<bool> Update(Guid id, T entity)
    {
        var existingT = await dbContext.Set<T>().FindAsync(id);
        if (existingT == null)
            return false;
       
        dbContext.Update(entity);

        return true;
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await GetById(id);
        if (entity == null) return false;
        
        dbContext.Set<T>().Remove(entity);
        
        return true;
    }
}

