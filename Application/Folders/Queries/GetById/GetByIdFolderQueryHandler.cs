using Application.Dtos.Folders;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Folders.Queries.GetById;

public class GetByIdFolderQueryHandler (IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByIdFolderQuery, GetFolderDto>
{
    public async Task<GetFolderDto> Handle(GetByIdFolderQuery request, CancellationToken cancellationToken)
    {
        
        var folder = await unitOfWork.FoldersRepo.GetById(request.Id, cancellationToken);
        if (folder == null) return null;

        var folderDto = mapper.Map<GetFolderDto>(folder);
        
        // var folderDto = new GetFolderDto
        // {
        //     Name = folder.Name,
        //     Id = folder.Id,
        //     RelativePath = folder.RelativePath,
        //     ParentFolderId = folder.ParentFolderId == null ? null : folder.ParentFolderId,
        //     SubFoldersIds = folder.SubFolders?.Select(sf => sf.Id).ToList() ?? new List<Guid>(),
        //     FilesIds = folder.Files?.Select(f => f.Id).ToList() ?? new List<Guid>()
        // };

        return folderDto;
    }
}