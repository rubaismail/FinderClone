using Application.Folders.Commands.CreateFolder;
using Application.Interfaces;
using Domain.Entities;
using Domain.Storage;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Folders.Commands.Create;

public class CreateQueryHandler (IUnitOfWork unitOfWork, IOptions<StorageSettings> settings) : IRequestHandler<CreateQuery, Folder>
{
    public async Task<Folder> Handle(CreateQuery request, CancellationToken cancellationToken)
    {
        var newFolder = new Folder
        {
            Name = request.FolderDto.Name,
            ParentFolderId = request.FolderDto.ParentFolderId,
            RelativePath = request.FolderDto.RelativePath,
            CreationDate = DateTime.UtcNow
        };

        await unitOfWork.FoldersRepo.Add(newFolder, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        var basePath = settings.Value.BasePath;
        var safeFolderName = PathHelper.MakeSafeName(newFolder.Name);
        var fullPath = Path.Combine(basePath, newFolder.RelativePath ?? "", safeFolderName);
        Directory.CreateDirectory(fullPath);

        return newFolder;
    }
}