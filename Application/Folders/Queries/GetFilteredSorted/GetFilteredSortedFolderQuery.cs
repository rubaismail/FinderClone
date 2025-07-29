using Application.Dtos.Folders;
using MediatR;

namespace Application.Folders.Queries.GetFilteredSorted;

public class GetFilteredSortedFolderQuery : IRequest<List<GetManyFoldersDto>>
{
    public DynamicFilterSortDto Filter { get; set; } = null!;
    // public string? FilterBy { get; set; } 
    // public FilterOperation? Operation { get; set; }
    // public string? Value { get; set; } 
    //
    // public string? SortBy { get; set; } 
    // public bool? SortDescending { get; set; } //= false;
    // public GetFilteredSortedFolderQuery() {}
}