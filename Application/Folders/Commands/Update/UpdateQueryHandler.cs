using Application.Dtos.Folders;
using Application.Interfaces;
using Domain.Entities;
using Domain.Storage;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Folders.Commands.Update;

public class UpdateQueryHandler(IUnitOfWork unitOfWork, IOptions<StorageSettings> settings)
    : IRequest<UpdateQuery>
{
    public async Task<bool> Handle(UpdateQuery request, CancellationToken cancellationToken)
    {
        var originalFolder = await unitOfWork.FoldersRepo.GetById(request.Id, cancellationToken);
        var updatedFolder = new Folder
        {
            Name = request.FolderDto.Name == null ? originalFolder.Name : request.FolderDto.Name,
            ParentFolderId =
                request.FolderDto.ParentFolderId == null
                    ? originalFolder.ParentFolderId
                    : request.FolderDto.ParentFolderId,
            RelativePath = request.FolderDto.RelativePath == null
                ? originalFolder.RelativePath
                : request.FolderDto.RelativePath,
            CreationDate = originalFolder.CreationDate
            // SubFolders = 
            // Files = 
        };
        var basePath = settings.Value.BasePath;
        var fullSourcePath = Path.Combine(basePath, originalFolder.RelativePath ?? "", originalFolder.Name);
        var fullDestPath = Path.Combine(basePath, updatedFolder.RelativePath ?? "", updatedFolder.Name);
        Directory.Move(fullSourcePath, fullDestPath);

        var updated = await unitOfWork.FoldersRepo.Update(request.Id, updatedFolder, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        return updated;
    }
}