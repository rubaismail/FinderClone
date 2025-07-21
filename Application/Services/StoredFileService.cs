using Application.Dtos.Files;
using Application.Dtos.Folders;
using Application.Interfaces;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Storage;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class StoredFileService (IUnitOfWork unitOfWork, IOptions<StorageSettings> settings): IStoredFileService
{
    public async Task<List<GetFileDto>> GetAllFilesAsync(CancellationToken cancellationToken)
    {
        var files = await unitOfWork.FilesRepo.GetAll(cancellationToken);
        var filesDto = files.Select(f => new GetFileDto
        {
            Name = f.Name,
            Id = f.Id,
            ParentFolderId = f.ParentFolderId
        }).ToList();
        
        return filesDto;
    }
    
    public async Task<List<GetFileDto>> GetFilteredFilesAsync(DynamicFilterSortDto filter, CancellationToken cancellationToken)
    {
        var files = await unitOfWork.FilesRepo.GetFilteredSorted(filter, cancellationToken);

        var filesDto = files.Select(f => new GetFileDto
        {
            Name = f.Name,
            Id = f.Id,
            ParentFolderId = f.ParentFolderId,
            CreationDate = f.CreationDate,
            SizeBytes = f.SizeBytes
        }).ToList();

        return filesDto;
    }

    /*public async Task<List<GetFileDto>> GetFileByNameAsync(string name)
    {
        var files = await unitOfWork.FilesRepo.GetByName(name);
        // if (files.Count == 0) return null;
        var filesDto =  files.Select(f => new GetFileDto
        {
            Name = f.Name,
            Id = f.Id,
            ParentFolderId = f.ParentFolderId
        }).ToList();
        
        return filesDto;
    }*/

    /*
    public async Task<PaginatedResult<GetFileDto>> GetFilesFilteredAsync(FileFilterDto filter)
    {
        var files = await unitOfWork.FilesRepo.GetFilesFiltered(filter);

        var filesDto = files.Items.Select(f => new GetFileDto
        {
            Name = f.Name,
            Id = f.Id,
            ParentFolderId = f.ParentFolderId,
            SizeBytes = f.SizeBytes,
            CreationDate = f.CreationDate
        }).ToList();
        
        return new PaginatedResult<GetFileDto>(filesDto, files.TotalCount, filter.Page, filter.PageSize);
    }
    */
    public async Task<GetFileDto?> GetFileByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var file = await unitOfWork.FilesRepo.GetById(id, cancellationToken);
        if (file == null) return null;
        
        var fileDto = new GetFileDto
        {
            Name = file.Name,
            Id = file.Id,
            ParentFolderId = file.ParentFolderId,
            CreationDate = file.CreationDate,
            SizeBytes = file.SizeBytes
        };
        
        return fileDto;
    }

    public async Task<StoredFile> AddFileAsync(CreateFileDto createFileDto, CancellationToken cancellationToken)
    {
        var newFile = new StoredFile
        {
            Name = createFileDto.Name,
            ParentFolderId = createFileDto.ParentFolderId,
            RelativePath = createFileDto.RelativePath,
            CreationDate = DateTime.UtcNow
        };
        await unitOfWork.FilesRepo.Add(newFile, cancellationToken);
        
        var basePath = settings.Value.BasePath;
        var safeFileName = PathHelper.MakeSafeName(newFile.Name);
        var fullPath = Path.Combine(basePath, newFile.RelativePath ?? "", safeFileName);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
        using (File.Create(fullPath)) { }
        
        newFile.SizeBytes = new FileInfo(fullPath).Length;
        await unitOfWork.FilesRepo.Update(newFile.Id, newFile, cancellationToken);
        await unitOfWork.SaveChangesAsync(); 
        
        return newFile;
    }

    public async Task<bool> UpdateFileAsync(UpdateFileDto fileDto, Guid id, CancellationToken cancellationToken)
    {
        var originalFile = await unitOfWork.FilesRepo.GetById(id, cancellationToken);
        var updatedFile = new StoredFile
        {
            Name = fileDto.Name == null ? originalFile.Name : fileDto.Name,
            ParentFolderId = fileDto.ParentFolderId == null ? originalFile.ParentFolderId : fileDto.ParentFolderId,
            RelativePath = fileDto.RelativePath == null ? originalFile.RelativePath : fileDto.RelativePath,
            CreationDate = originalFile.CreationDate,
            SizeBytes = fileDto.SizeBytes == null ? originalFile.SizeBytes : fileDto.SizeBytes
        };
        var basePath = settings.Value.BasePath;
        var fullSourcePath = Path.Combine(basePath, originalFile.RelativePath ?? "", originalFile.Name);
        var fullDestPath = Path.Combine(basePath, updatedFile.RelativePath ?? "", updatedFile.Name);
        File.Move(fullSourcePath, fullDestPath);
        
        var updated = await unitOfWork.FilesRepo.Update(id, updatedFile, cancellationToken);
        await unitOfWork.SaveChangesAsync(); 
        
        return updated;
    }

    public async Task<bool> DeleteFileAsync(Guid id, CancellationToken cancellationToken)
    {
        var file = await unitOfWork.FilesRepo.GetById(id, cancellationToken);
        bool deleted = false;
        if (file != null)
        {
            var basePath = settings.Value.BasePath;
            var fullPath = Path.Combine(basePath, file.RelativePath ?? "", file.Name);
            Directory.Delete(fullPath, true);
            deleted = await unitOfWork.FilesRepo.Delete(id, cancellationToken); 
        }
        await unitOfWork.SaveChangesAsync(); 
        return deleted;
    }
}