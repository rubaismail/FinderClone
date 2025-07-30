using Application.Interfaces;
using Domain.Storage;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Folders.Commands.Delete;

public class DeleteFolderCommandHandler (IUnitOfWork unitOfWork, IOptions<StorageSettings> settings) : IRequestHandler<DeleteFolderCommand, bool>
{
    public async Task<bool> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
    {
        var folder = await unitOfWork.FoldersRepo.GetById(request.Id, cancellationToken);
        bool deleted = false;
        if (folder != null)
        {
            var basePath = settings.Value.BasePath;
            var fullPath = Path.Combine(basePath, folder.RelativePath ?? "", folder.Name);
            Directory.Delete(fullPath, true);
            //PathHelper.DeleteFolderRecursively(fullPath);
            
            folder.DeletedOn = DateTime.UtcNow;
            deleted = true;
            //deleted = await unitOfWork.FoldersRepo.Delete(request.Id, cancellationToken);
            await unitOfWork.SaveChangesAsync();
        }

        return deleted;
    }
}