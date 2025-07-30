using Application.Dtos.Folders;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IFolderService
{
    Task<List<GetManyFoldersDto>> GetAllFoldersAsync(CancellationToken cancellationToken);
    Task<List<GetManyFoldersDto>> GetFilteredFoldersAsync(DynamicFilterSortDto filter, CancellationToken cancellationToken);
    Task<GetFolderDto?> GetFolderByIdAsync(Guid id, CancellationToken cancellationToken);
    // Task<List<GetManyFoldersDto>> GetFoldersByNameAsync(string name);
    Task<Folder> AddFolderAsync(CreateFolderDto folderDto, CancellationToken cancellationToken);
    Task<bool> UpdateFolderAsync(UpdateFolderDto folderDto, Guid id, CancellationToken cancellationToken);
    Task<bool> DeleteFolderAsync(Guid id, CancellationToken cancellationToken);
}