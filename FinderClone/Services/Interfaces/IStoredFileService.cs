using FinderClone.Dtos;
using FinderClone.Models;

namespace FinderClone.Services.Interfaces;

public interface IStoredFileService
{
    Task<List<GetFileDto>> GetAllFilesAsync();
    Task<List<GetFileDto>> GetFileByNameAsync(String name);
    Task <GetFileDto?> GetFileByIdAsync(Guid id);
    Task <StoredFile> AddFileAsync(CreateFileDto createFileDto);
    Task <bool> UpdateFileAsync(UpdateFileDto fileDto, Guid id);
    Task <bool> DeleteFileAsync(Guid id);
}
        
     