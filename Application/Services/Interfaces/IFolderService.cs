using Application.Dtos.Folders;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IFolderService
{
    Task<List<GetManyFoldersDto>> GetAllFoldersAsync();
    Task<List<GetManyFoldersDto>> GetFilteredFoldersAsync(DynamicFilterSortDto filter);
    Task<GetFolderDto?> GetFolderByIdAsync(Guid id);
    // Task<List<GetManyFoldersDto>> GetFoldersByNameAsync(string name);
    Task<Folder> AddFolderAsync(CreateFolderDto folderDto);
    Task<bool> UpdateFolderAsync(UpdateFolderDto folderDto, Guid id);
    Task<bool> DeleteFolderAsync(Guid id);
}