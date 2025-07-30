using Application.Dtos.Files;
using Application.Dtos.Folders;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IStoredFileService
{
    Task<List<GetFileDto>> GetAllFilesAsync(CancellationToken cancellationToken);
    //Task<List<GetFileDto>> GetFileByNameAsync(String name);
    Task<List<GetFileDto>> GetFilteredFilesAsync(DynamicFilterSortDto filter, CancellationToken cancellationToken);
    //Task <PaginatedResult<GetFileDto>> GetFilesFilteredAsync(FileFilterDto filter);
    Task <GetFileDto?> GetFileByIdAsync(Guid id, CancellationToken cancellationToken);
    Task <StoredFile> AddFileAsync(CreateFileDto createFileDto, CancellationToken cancellationToken);
    Task <bool> UpdateFileAsync(UpdateFileDto fileDto, Guid id, CancellationToken cancellationToken);
    Task <bool> DeleteFileAsync(Guid id, CancellationToken cancellationToken);
}
        
     