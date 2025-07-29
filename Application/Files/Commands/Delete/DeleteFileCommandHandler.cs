using Application.Interfaces;
using Domain.Storage;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Files.Commands.Delete;

public class DeleteFileCommandHandler (IUnitOfWork unitOfWork, IOptions<StorageSettings> settings) : IRequestHandler<DeleteFileCommand, bool>
{
    public async Task<bool> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var file = await unitOfWork.FilesRepo.GetById(request.Id, cancellationToken);
        bool deleted = false;
        if (file != null)
        {
            var basePath = settings.Value.BasePath;
            var fullPath = Path.Combine(basePath, file.RelativePath ?? "", file.Name);
            Directory.Delete(fullPath, true);
            file.DeletedOn = DateTime.UtcNow;
            deleted = true;
            //deleted = await unitOfWork.FilesRepo.Delete(request.Id, cancellationToken); 
            await unitOfWork.SaveChangesAsync(); 
        }
        return deleted;
    }
}