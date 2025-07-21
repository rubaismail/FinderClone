using Application.Dtos.Files;
using Application.Dtos.Folders;
using MediatR;

namespace Application.Files.Queries.GetFilteredSorted;

public class GetFilteredSortedQuery : IRequest<List<GetFileDto>>
{
    public DynamicFilterSortDto Filter { get; set; } = null!;
}