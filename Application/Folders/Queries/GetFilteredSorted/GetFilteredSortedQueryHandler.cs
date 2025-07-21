using Application.Dtos.Folders;
using Application.Interfaces;
using MediatR;

namespace Application.Folders.Queries.GetFilteredSorted;

public class GetFilteredSortedQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetFilteredSortedQuery, List<GetManyFoldersDto>>
{
    public async Task<List<GetManyFoldersDto>> Handle(GetFilteredSortedQuery request, CancellationToken cancellationToken)
    {
        var folders = await unitOfWork.FoldersRepo.GetFilteredSorted(request.Filter, cancellationToken);

        var foldersDto = folders.Select(f => new GetManyFoldersDto
        {
            Name = f.Name,
            Id = f.Id,
        }).ToList();

        return foldersDto;
    }
}