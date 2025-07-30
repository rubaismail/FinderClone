using Application.Dtos.Folders;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Folders.Queries.GetAll;

public class GetAllFolderQueryHandler (IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllFolderQuery, List<GetManyFoldersDto>>
{
    public async Task<List<GetManyFoldersDto>> Handle(GetAllFolderQuery request, CancellationToken cancellationToken)
    {
        var folders = await unitOfWork.FoldersRepo.GetAll(cancellationToken);
        
        var foldersDto = mapper.Map<List<GetManyFoldersDto>>(folders);
        // var foldersDto = folders.Select(f => new GetManyFoldersDto
        // {
        //     Name = f.Name,
        //     Id = f.Id,
        // }).ToList();

        return foldersDto;
    }
}