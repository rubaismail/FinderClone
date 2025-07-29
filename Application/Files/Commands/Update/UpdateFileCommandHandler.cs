using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Storage;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Files.Commands.Update;

public class UpdateFileCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IOptions<StorageSettings> settings) 
    : IRequest<UpdateFileCommand>
{
    public async Task<bool> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
    {
        var originalFile = await unitOfWork.FilesRepo.GetById(request.Id, cancellationToken);
        if (originalFile == null)
            return false;
        var updatedFile = mapper.Map<StoredFile>(request);
        // var updatedFile = new StoredFile
        // {
        //     Name = request.Name == null ? originalFile.Name : request.Name,
        //     ParentFolderId = request.ParentFolderId == null
        //         ? originalFile.ParentFolderId
        //         : request.ParentFolderId,
        //     RelativePath = request.RelativePath == null
        //         ? originalFile.RelativePath
        //         : request.RelativePath,
        //     CreationDate = originalFile.CreationDate,
        //     SizeBytes = request.SizeBytes == null ? originalFile.SizeBytes : request.SizeBytes
        // };
        var basePath = settings.Value.BasePath;
        var fullSourcePath = Path.Combine(basePath, originalFile.RelativePath ?? "", originalFile.Name);
        var fullDestPath = Path.Combine(basePath, updatedFile.RelativePath ?? "", updatedFile.Name);
        File.Move(fullSourcePath, fullDestPath);
        updatedFile.CreatedOn = DateTime.UtcNow;
        var updated = await unitOfWork.FilesRepo.Update(request.Id, updatedFile, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        return updated;
    }
}