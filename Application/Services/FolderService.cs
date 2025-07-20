using Application.Dtos.Folders;
using Application.Interfaces;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Storage;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class FolderService(IUnitOfWork unitOfWork, IOptions<StorageSettings> settings) : IFolderService
{
    public async Task<List<GetManyFoldersDto>> GetAllFoldersAsync()
    {
        var folders = await unitOfWork.FoldersRepo.GetAll();
        var foldersDto = folders.Select(f => new GetManyFoldersDto
        {
            Name = f.Name,
            Id = f.Id,
        }).ToList();

        return foldersDto;
    }
    
    public async Task<List<GetManyFoldersDto>> GetFilteredFoldersAsync(DynamicFilterSortDto filter)
    {
        var folders = await unitOfWork.FoldersRepo.GetFilteredSorted(filter);

        var foldersDto = folders.Select(f => new GetManyFoldersDto
        {
            Name = f.Name,
            Id = f.Id,
        }).ToList();

        return foldersDto;
    }

    public async Task<GetFolderDto?> GetFolderByIdAsync(Guid id)
    {
        var folder = await unitOfWork.FoldersRepo.GetById(id);
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
    
    /*public async Task<List<GetManyFoldersDto>> GetFoldersByNameAsync(string name)
    {
        var folders = await unitOfWork.FoldersRepo.GetByName(name);
        //if (folders.Count == 0)
        //return null;

        var foldersDto = folders.Select(f => new GetManyFoldersDto
        {
            Name = f.Name,
            Id = f.Id,
        }).ToList();

        return foldersDto;
    }*/

   /*
    public async Task<PaginatedResult<GetManyFoldersDto>> GetFoldersByNameFilteredAsync(FolderFilterDto filter)
    {
        var folders = await unitOfWork.FoldersRepo.GetFolderByNameFiltered(filter);

        var foldersDto = folders.Items.Select(f => new GetManyFoldersDto
        {
            Name = f.Name,
            Id = f.Id,
        }).ToList();

        return new PaginatedResult<GetManyFoldersDto>(foldersDto, folders.TotalCount, filter.Page, filter.PageSize);
    }
    */

    public async Task<Folder> AddFolderAsync(CreateFolderDto folderDto)
    {
        var newFolder = new Folder
        {
            Name = folderDto.Name,
            ParentFolderId = folderDto.ParentFolderId,
            RelativePath = folderDto.RelativePath,
            CreationDate = DateTime.UtcNow
        };

        await unitOfWork.FoldersRepo.Add(newFolder);
        await unitOfWork.Save();

        var basePath = settings.Value.BasePath;
        var safeFolderName = PathHelper.MakeSafeName(newFolder.Name);
        var fullPath = Path.Combine(basePath, newFolder.RelativePath ?? "", safeFolderName);
        Directory.CreateDirectory(fullPath);

        return newFolder;
    }

    public async Task<bool> UpdateFolderAsync(UpdateFolderDto folderDto, Guid id)
    {
        var originalFolder = await unitOfWork.FoldersRepo.GetById(id);
        var updatedFolder = new Folder
        {
            Name = folderDto.Name == null ? originalFolder.Name : folderDto.Name,
            ParentFolderId =
                folderDto.ParentFolderId == null ? originalFolder.ParentFolderId : folderDto.ParentFolderId,
            RelativePath = folderDto.RelativePath == null ? originalFolder.RelativePath : folderDto.RelativePath,
            CreationDate = originalFolder.CreationDate
            // SubFolders = 
            // Files = 
        };
        var basePath = settings.Value.BasePath;
        var fullSourcePath = Path.Combine(basePath, originalFolder.RelativePath ?? "", originalFolder.Name);
        var fullDestPath = Path.Combine(basePath, updatedFolder.RelativePath ?? "", updatedFolder.Name);
        Directory.Move(fullSourcePath, fullDestPath);

        var updated = await unitOfWork.FoldersRepo.Update(id, updatedFolder);
        await unitOfWork.Save();
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
            Directory.Delete(fullPath, true);
            //PathHelper.DeleteFolderRecursively(fullPath);
            
            deleted = await unitOfWork.FoldersRepo.Delete(id);
            await unitOfWork.Save();
        }

        return deleted;
    }
}