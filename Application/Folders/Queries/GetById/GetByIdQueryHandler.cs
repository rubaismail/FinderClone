using Application.Dtos.Folders;
using Application.Interfaces;
using MediatR;

namespace Application.Folders.Queries.GetById;

public class GetByIdQueryHandler (IUnitOfWork unitOfWork) : IRequestHandler<GetByIdQuery, GetFolderDto>
{
    public async Task<GetFolderDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var folder = await unitOfWork.FoldersRepo.GetById(request.Id, cancellationToken);
        if (folder == null) return null;
        var folderDto = new GetFolderDto
        {
            Name = folder.Name,
            Id = folder.Id,
            RelativePath = folder.RelativePath,
            ParentFolderId = folder.ParentFolderId == null ? null : folder.ParentFolderId,
            SubFoldersIds = folder.SubFolders?.Select(sf => sf.Id).ToList() ?? new List<Guid>(),
            FilesIds = folder.Files?.Select(f => f.Id).ToList() ?? new List<Guid>()
        };

        return folderDto;
    }
}