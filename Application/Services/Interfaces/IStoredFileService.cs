using Application.Dtos.Files;
using Application.Dtos.Folders;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IStoredFileService
{
    Task<List<GetFileDto>> GetAllFilesAsync();
    //Task<List<GetFileDto>> GetFileByNameAsync(String name);
    Task<List<GetFileDto>> GetFilteredFilesAsync(DynamicFilterSortDto filter);
    //Task <PaginatedResult<GetFileDto>> GetFilesFilteredAsync(FileFilterDto filter);
    Task <GetFileDto?> GetFileByIdAsync(Guid id);
    Task <StoredFile> AddFileAsync(CreateFileDto createFileDto);
    Task <bool> UpdateFileAsync(UpdateFileDto fileDto, Guid id);
    Task <bool> DeleteFileAsync(Guid id);
}
        
     