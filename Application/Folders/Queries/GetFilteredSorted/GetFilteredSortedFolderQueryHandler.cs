using Application.Dtos.Folders;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Folders.Queries.GetFilteredSorted;

public class GetFilteredSortedFolderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetFilteredSortedFolderQuery, List<GetManyFoldersDto>>
{
    public async Task<List<GetManyFoldersDto>> Handle(GetFilteredSortedFolderQuery request, CancellationToken cancellationToken)
    {
        //var requestDto = mapper.Map<DynamicFilterSortDto>(request);
        var folders = await unitOfWork.FoldersRepo.GetFilteredSorted(request.Filter, cancellationToken);

        var foldersDto = mapper.Map<List<GetManyFoldersDto>>(folders);
        // var foldersDto = folders.Select(f => new GetManyFoldersDto
        // {
        //     Name = f.Name,
        //     Id = f.Id,
        // }).ToList();

        return foldersDto;
    }
}