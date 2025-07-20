// using Web.Data;
// using Web.Dtos;
// using Web.Extensions;
// using Web.Models;
// using Web.Repositories.Interfaces;
// using Microsoft.EntityFrameworkCore;
//
// namespace Web.Repositories;
//
// public class StoredFileRepository(AppDbContext dbContext) : IStoredFileRepository
// {
//     public async Task<List<StoredFile>> GetAllFiles()
//     {
//         return await dbContext.Files.ToListAsync();
//     }
//     
//     public async Task<List<StoredFile>> GetFilteredFiles(DynamicFilterSortDto filter)
//     {
//         if (string.IsNullOrEmpty(filter.SortBy) && string.IsNullOrEmpty(filter.FilterBy))
//             return await dbContext.Files.ToListAsync();
//         
//         else if (string.IsNullOrEmpty(filter.SortBy))
//         {
//             var query = dbContext.Files.AsQueryable()
//                 .ApplyDynamicFilter(filter);
//             return await query.ToListAsync();
//         }
//         else if (string.IsNullOrEmpty(filter.FilterBy))
//         {
//             var query2 = dbContext.Files.AsQueryable()
//                 .ApplyDynamicSorting(filter);
//             return await query2.ToListAsync();
//         }
//         
//         var query3 = dbContext.Files.AsQueryable()
//             .ApplyDynamicFilter(filter)
//             .ApplyDynamicSorting(filter);
//
//         return await query3.ToListAsync();
//     }
//
//     public async Task<List<StoredFile>> GetFileByName(string name)
//     {
//         return await dbContext.Files
//             .Where(f  => EF.Functions.ILike(f.Name, $"%{name}%"))
//             .ToListAsync();
//     }
//     
//     /* public async Task<PaginatedResult<StoredFile>> GetFilesFiltered(FileFilterDto filter)
//     {
//         var query = dbContext.Files.AsQueryable()
//             .ApplyFiltering(filter)
//             .ApplySorting(filter);
//
//         var count = await query.CountAsync();
//         
//         var files = await query
//             .Paginate(filter.Page, filter.PageSize)
//             .ToListAsync();
//
//         return new PaginatedResult<StoredFile>(files, count, filter.Page, filter.PageSize);
//     }
// */
//     public async Task<StoredFile?> GetFileById(Guid id)
//     {
//         return await dbContext.Files
//             .FirstOrDefaultAsync(f => f.Id == id);
//     }
//
//     public async Task<StoredFile> AddFile(StoredFile file)
//     {
//         await dbContext.Files.AddAsync(file);
//         //await dbContext.SaveChangesAsync();
//         return file;
//     }
//
//     public async Task<bool> UpdateFile(Guid id, StoredFile file)
//     {
//         var currentFile = await dbContext.Files.FindAsync(id);
//         
//         if (currentFile == null)
//             return false;
//         else
//         {
//             currentFile.Name = file.Name;
//             currentFile.ParentFolderId = file.ParentFolderId;
//             currentFile.CreationDate = file.CreationDate;
//             currentFile.SizeBytes = file.SizeBytes;
//             return true;
//         }
//     }
//
//     public async Task<bool> DeleteFile(Guid id)
//     {
//         var file = await dbContext.Files.FindAsync(id);
//         
//         if (file == null)
//             return false;
//         else
//         {
//             dbContext.Files.Remove(file);
//             //await dbContext.SaveChangesAsync();
//             return true;
//         }
//     }
// }