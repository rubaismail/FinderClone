using Application.Dtos.Folders;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Storage;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Folders.Commands.Update;

public class UpdateFolderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IOptions<StorageSettings> settings)
    : IRequest<UpdateFolderCommand>
{
    public async Task<bool> Handle(UpdateFolderCommand request, CancellationToken cancellationToken)
    {
        var originalFolder = await unitOfWork.FoldersRepo.GetById(request.Id, cancellationToken);
        // var updatedFolder = new Folder
        // {
        //     Name = request.Name == null ? originalFolder.Name : request.Name,
        //     ParentFolderId =
        //         request.ParentFolderId == null
        //             ? originalFolder.ParentFolderId
        //             : request.ParentFolderId,
        //     RelativePath = request.RelativePath == null
        //         ? originalFolder.RelativePath
        //         : request.RelativePath,
        //     CreationDate = originalFolder.CreationDate
        //     // SubFolders = 
        //     // Files = 
        // };
        var updatedFolder = mapper.Map<Folder>(request);
        
        var basePath = settings.Value.BasePath;
        var fullSourcePath = Path.Combine(basePath, originalFolder.RelativePath ?? "", originalFolder.Name);
        var fullDestPath = Path.Combine(basePath, updatedFolder.RelativePath ?? "", updatedFolder.Name);
        Directory.Move(fullSourcePath, fullDestPath);
        
        updatedFolder.UpdatedOn = DateTime.UtcNow;
        var updated = await unitOfWork.FoldersRepo.Update(request.Id, updatedFolder, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        return updated;
    }
}