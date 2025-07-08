using FinderClone.Dtos;
using FinderClone.Models;
using FinderClone.Repositories.Interfaces;
using FinderClone.Services.Interfaces;
using FinderClone.Storage;
using Microsoft.Extensions.Options;

namespace FinderClone.Services;

public class FolderService (IFolderRepository folderRepository, IOptions<StorageSettings> settings) : IFolderService
{
    public async Task<List<GetManyFoldersDto>> GetAllFoldersAsync()
    {
        var folders = await folderRepository.GetFolders();
        var foldersDto = folders.Select(f => new GetManyFoldersDto
        {
            Name = f.Name,
            Id = f.Id,
        }).ToList();
        
        return foldersDto;
    }

    public async Task<List<GetManyFoldersDto>> GetFoldersByNameAsync(string name)
    {
        var folders = await folderRepository.GetFolderByName(name);
        //if (folders.Count == 0)
            //return null;
            
        var foldersDto = folders.Select(f => new GetManyFoldersDto
        {
            Name = f.Name,
            Id = f.Id,
        }).ToList();

        return foldersDto;
    }

    public async Task<GetFolderDto?> GetFolderByIdAsync(Guid id)
    {
        var folder = await folderRepository.GetFolderById(id);
        if (folder == null) return null;
        var folderDto = new GetFolderDto
        {
            Name = folder.Name,
            Id = folder.Id,
            RelativePath = folder.RelativePath,
            ParentFolderId = folder.ParentFolderId == null ? null : folder.ParentFolderId,
            SubFoldersIds = folder.SubFolders?.Select(sf => sf.Id).ToList() ?? new List<Guid>(),
            FilesIds = folder.Files?.Select(f => f.Id).ToList() ?? new List<Guid>()
        };
        
        return folderDto;
    }

    public async Task<Folder> AddFolderAsync(CreateFolderDto folderDto)
    {
        var newFolder = new Folder
        {
            Name = folderDto.Name,
            ParentFolderId = folderDto.ParentFolderId,
            RelativePath = folderDto.RelativePath
        };
        await folderRepository.AddFolder(newFolder);
        
        var basePath = settings.Value.BasePath;
        var safeFolderName = PathHelper.MakeSafeName(newFolder.Name);
        var fullPath = Path.Combine(basePath, newFolder.RelativePath ?? "", safeFolderName);
        Directory.CreateDirectory(fullPath);
        
        return newFolder;
    }

    public async Task<bool> UpdateFolderAsync(UpdateFolderDto folderDto, Guid id)
    {
        var updatedFolder = new Folder
        {
            Name = folderDto.Name,
            ParentFolderId = folderDto.ParentFolderId == null ? null : folderDto.ParentFolderId,
            // SubFolders = 
            // Files = 
        };
        var updated = await folderRepository.UpdateFolder(id, updatedFolder);
        
        return updated;
    }

    public async Task<bool> DeleteFolderAsync(Guid id)
    {
        var folder = await GetFolderByIdAsync(id);
        bool deleted = false;
        if (folder != null)
        {
            var basePath = settings.Value.BasePath;
            var fullPath = Path.Combine(basePath, folder.RelativePath ?? "", folder.Name);
            //Directory.Delete(fullPath, true);
            PathHelper.DeleteFolderRecursively(fullPath);
            deleted = await folderRepository.DeleteFolder(id);
        }
        
        return deleted;
    }
}