using Application.Interfaces;
using Domain.Storage;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Folders.Commands.Delete;

public class DeleteQueryHandler (IUnitOfWork unitOfWork, IOptions<StorageSettings> settings) : IRequestHandler<DeleteQuery, bool>
{
    public async Task<bool> Handle(DeleteQuery request, CancellationToken cancellationToken)
    {
        var folder = await unitOfWork.FoldersRepo.GetById(request.Id, cancellationToken);
        bool deleted = false;
        if (folder != null)
        {
            var basePath = settings.Value.BasePath;
            var fullPath = Path.Combine(basePath, folder.RelativePath ?? "", folder.Name);
            Directory.Delete(fullPath, true);
            //PathHelper.DeleteFolderRecursively(fullPath);
            
            deleted = await unitOfWork.FoldersRepo.Delete(request.Id, cancellationToken);
            await unitOfWork.SaveChangesAsync();
        }

        return deleted;
    }
}