// using Web.Data;
// using Web.Dtos;
// using Web.Extensions;
// using Web.Models;
// using Web.Repositories.Interfaces;
// using Web.UnitOfWork;
// using Microsoft.EntityFrameworkCore;
//
// namespace Web.Repositories;
//
// public class FolderRepository(AppDbContext dbContext) : IFolderRepository
// {
//     public async Task<List<Folder>> GetFolders()
//     {
//         return await dbContext.Folders.ToListAsync();
//     }
//     
//     public async Task<List<Folder>> GetFilteredFolders(DynamicFilterSortDto filter)
//     {
//         if (string.IsNullOrEmpty(filter.SortBy) && string.IsNullOrEmpty(filter.FilterBy))
//             return await dbContext.Folders.ToListAsync();
//         
//         else if (string.IsNullOrEmpty(filter.SortBy))
//         {
//             var query = dbContext.Folders.AsQueryable()
//                 .ApplyDynamicFilter(filter);
//             return await query.ToListAsync();
//         }
//         else if (string.IsNullOrEmpty(filter.FilterBy))
//         {
//             var query2 = dbContext.Folders.AsQueryable()
//                 .ApplyDynamicSorting(filter);
//             return await query2.ToListAsync();
//         }
//         
//         var query3 = dbContext.Folders.AsQueryable()
//             .ApplyDynamicFilter(filter)
//             .ApplyDynamicSorting(filter);
//
//         return await query3.ToListAsync();
//     }
//     
//     public async Task<List<Folder>> GetFolderByName(string name)
//     {
//         return await dbContext.Folders
//             .Where(f => EF.Functions.ILike(f.Name, $"%{name}%")).ToListAsync();
//     }
//
//     /* public async Task<PaginatedResult<Folder>> GetFolderByNameFiltered(FolderFilterDto filter)
//     {
//         var query = dbContext.Folders
//             .Where(f => EF.Functions.ILike(f.Name, $"%{filter.Name}%"))
//             .ApplyFiltering(filter)
//             .ApplySorting(filter);
//             //.Paginate(page, pageSize);
//
//         var count = await query.CountAsync();
//         
//         var folders = await query
//             .Paginate(filter.Page, filter.PageSize)
//             .ToListAsync();
//
//         //var folders = await query.ToListAsync();
//
//         return new PaginatedResult<Folder>(folders, count, filter.Page, filter.PageSize);
//     }
//     */
//
//     public async Task<Folder?> GetFolderById(Guid id)
//     {
//         return await dbContext.Folders
//             .FirstOrDefaultAsync(f => f.Id == id);
//     }
//
//     public async Task<Folder> AddFolder(Folder folder)
//     {
//         dbContext.Folders.Add(folder);
//         // await dbContext.SaveChangesAsync();
//
//         return folder;
//     }
//
//     public async Task<bool> UpdateFolder(Guid id, Folder folder)
//     {
//         var existingFolder = await dbContext.Folders.FindAsync(id);
//         if (existingFolder == null) return false;
//
//         /*
//          existingFolder.Name = folder.Name;
//
//         if (folder.ParentFolderId != null)
//             existingFolder.ParentFolderId = folder.ParentFolderId;
//
//         if (folder.SubFolders != null)
//             existingFolder.SubFolders = folder.SubFolders;
//
//         if (folder.Files != null)
//             existingFolder.Files = folder.Files;
//     */
//         dbContext.Update(folder);
//         //await dbContext.SaveChangesAsync();
//
//         return true;
//     }
//
//     public async Task<bool> DeleteFolder(Guid id)
//     {
//         var folder = await dbContext.Folders.FindAsync(id);
//
//         if (folder == null) return false;
//
//         dbContext.Folders.Remove(folder);
//         // cascade delete is automatically on in ef core?
//         //await dbContext.SaveChangesAsync();
//
//         return true;
//     }
// }