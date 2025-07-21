using Application.Interfaces;
using Domain.Entities;
using Domain.Storage;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Files.Commands.Update;

public class UpdateQueryHandler(IUnitOfWork unitOfWork, IOptions<StorageSettings> settings) : IRequest<UpdateQuery>
{
    public async Task<bool> Handle(UpdateQuery request, CancellationToken cancellationToken)
    {
        var originalFile = await unitOfWork.FilesRepo.GetById(request.Id, cancellationToken);
        var updatedFile = new StoredFile
        {
            Name = request.FileDto.Name == null ? originalFile.Name : request.FileDto.Name,
            ParentFolderId = request.FileDto.ParentFolderId == null
                ? originalFile.ParentFolderId
                : request.FileDto.ParentFolderId,
            RelativePath = request.FileDto.RelativePath == null
                ? originalFile.RelativePath
                : request.FileDto.RelativePath,
            CreationDate = originalFile.CreationDate,
            SizeBytes = request.FileDto.SizeBytes == null ? originalFile.SizeBytes : request.FileDto.SizeBytes
        };
        var basePath = settings.Value.BasePath;
        var fullSourcePath = Path.Combine(basePath, originalFile.RelativePath ?? "", originalFile.Name);
        var fullDestPath = Path.Combine(basePath, updatedFile.RelativePath ?? "", updatedFile.Name);
        File.Move(fullSourcePath, fullDestPath);

        var updated = await unitOfWork.FilesRepo.Update(request.Id, updatedFile, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        return updated;
    }
}