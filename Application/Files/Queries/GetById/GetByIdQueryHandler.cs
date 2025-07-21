using Application.Dtos.Files;
using Application.Dtos.Folders;
using Application.Interfaces;
using MediatR;

namespace Application.Files.Queries.GetById;

public class GetByIdQueryHandler (IUnitOfWork unitOfWork) : IRequestHandler<GetByIdQuery, GetFileDto>
{
    public async Task<GetFileDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var file = await unitOfWork.FilesRepo.GetById(request.Id, cancellationToken);
        if (file == null) return null;
        
        var fileDto = new GetFileDto
        {
            Name = file.Name,
            Id = file.Id,
            ParentFolderId = file.ParentFolderId,
            CreationDate = file.CreationDate,
            SizeBytes = file.SizeBytes
        };
        
        return fileDto;
    }
}