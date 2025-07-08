using FinderClone.Dtos;
using FinderClone.Models;
using FinderClone.Repositories.Interfaces;
using FinderClone.Services.Interfaces;
using FinderClone.Storage;
using Microsoft.Extensions.Options;

namespace FinderClone.Services;

public class StoredFileService (IStoredFileRepository storedFileRepository, IOptions<StorageSettings> settings): IStoredFileService
{
    public async Task<List<GetFileDto>> GetAllFilesAsync()
    {
        var files = await storedFileRepository.GetAllFiles();
        var filesDto = files.Select(f => new GetFileDto
        {
            Name = f.Name,
            Id = f.Id,
            ParentFolderId = f.ParentFolderId
        }).ToList();
        
        return filesDto;
    }

    public async Task<List<GetFileDto>> GetFileByNameAsync(string name)
    {
        var files = await storedFileRepository.GetFileByName(name);
        // if (files.Count == 0) return null;
        var filesDto =  files.Select(f => new GetFileDto
        {
            Name = f.Name,
            Id = f.Id,
            ParentFolderId = f.ParentFolderId
        }).ToList();
        
        return filesDto;
    }

    public async Task<GetFileDto?> GetFileByIdAsync(Guid id)
    {
        var file = await storedFileRepository.GetFileById(id);
        if (file == null) return null;
        
        var fileDto = new GetFileDto
        {
            Name = file.Name,
            Id = file.Id,
            ParentFolderId = file.ParentFolderId
        };
        
        return fileDto;
    }

    public async Task<StoredFile> AddFileAsync(CreateFileDto createFileDto)
    {
        var newFile = new StoredFile
        {
            Name = createFileDto.Name,
            ParentFolderId = createFileDto.ParentFolderId,
            RelativePath = createFileDto.RelativePath
        };
        await storedFileRepository.AddFile(newFile);
        
        var basePath = settings.Value.BasePath;
        var safeFileName = PathHelper.MakeSafeName(newFile.Name);
        var fullPath = Path.Combine(basePath, newFile.RelativePath ?? "", safeFileName);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
        using (File.Create(fullPath)) { }
        
        return newFile;
    }

    public async Task<bool> UpdateFileAsync(UpdateFileDto fileDto, Guid id)
    {
        var updatedFile = new StoredFile
        {
            Name = fileDto.Name,
            ParentFolderId = fileDto.ParentFolderId
        };
        var updated = await storedFileRepository.UpdateFile(id, updatedFile);
        
        return updated;
    }

    public async Task<bool> DeleteFileAsync(Guid id)
    {
        var deleted = await storedFileRepository.DeleteFile(id);
        
        return deleted;
    }
}