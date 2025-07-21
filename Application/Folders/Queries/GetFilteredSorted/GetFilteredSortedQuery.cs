using Application.Dtos.Folders;
using MediatR;

namespace Application.Folders.Queries.GetFilteredSorted;

public class GetFilteredSortedQuery : IRequest<List<GetManyFoldersDto>>
{
    public DynamicFilterSortDto Filter { get; set; } = null!;
}