using Application.Interfaces;
using Domain.Entities;
using Domain.Storage;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Files.Commands.Create;

public class CreateQueryHandler(IUnitOfWork unitOfWork, IOptions<StorageSettings> settings) : IRequestHandler<CreateQuery, StoredFile>
{
    public async Task<StoredFile> Handle(CreateQuery request, CancellationToken cancellationToken)
    {
        var newFile = new StoredFile
        {
            Name = request.Name,
            ParentFolderId = request.ParentFolderId,
            RelativePath = request.RelativePath,
            CreationDate = DateTime.UtcNow
        };
        await unitOfWork.FilesRepo.Add(newFile, cancellationToken);
        
        var basePath = settings.Value.BasePath;
        var safeFileName = PathHelper.MakeSafeName(newFile.Name);
        var fullPath = Path.Combine(basePath, newFile.RelativePath ?? string.Empty, safeFileName);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
        await using (File.Create(fullPath)) { }
        
        newFile.SizeBytes = new FileInfo(fullPath).Length;
        await unitOfWork.FilesRepo.Update(newFile.Id, newFile, cancellationToken);
        await unitOfWork.SaveChangesAsync(); 
        
        return newFile;
    }
}