using FinderClone.Dtos;
using FinderClone.Models;

namespace FinderClone.Services.Interfaces;

public interface IFolderService
{
    Task <List<GetManyFoldersDto>> GetAllFoldersAsync();
    Task <List<GetManyFoldersDto>> GetFoldersByNameAsync(string name);
    Task <GetFolderDto?> GetFolderByIdAsync(Guid id);
    Task <Folder> AddFolderAsync(CreateFolderDto folderDto);
    Task <bool> UpdateFolderAsync(UpdateFolderDto folderDto, Guid id);
    Task <bool> DeleteFolderAsync(Guid id);
}