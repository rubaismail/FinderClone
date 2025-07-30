using Application.Folders.Commands.CreateFolder;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Storage;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Folders.Commands.Create;

public class CreateFolderCommandHandler (IUnitOfWork unitOfWork, IMapper mapper, IOptions<StorageSettings> settings) : IRequestHandler<CreateFolderCommand, Folder>
{
    public async Task<Folder> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
    {
        // var newFolder = new Folder
        // {
        //     Name = request.Name,
        //     ParentFolderId = request.ParentFolderId,
        //     RelativePath = request.RelativePath,
        //     CreationDate = DateTime.UtcNow
        // };
        
        var newFolder = mapper.Map<Folder>(request);
        newFolder.CreatedOn = DateTime.UtcNow;

        await unitOfWork.FoldersRepo.Add(newFolder, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        var basePath = settings.Value.BasePath;
        var safeFolderName = PathHelper.MakeSafeName(newFolder.Name);
        var fullPath = Path.Combine(basePath, newFolder.RelativePath ?? "", safeFolderName);
        Directory.CreateDirectory(fullPath);

        return newFolder;
    }
}