using Application.Dtos.Files;
using Application.Interfaces;
using MediatR;

namespace Application.Files.Queries.GetFilteredSorted;

public class GetFilteredSortedQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetFilteredSortedQuery, List<GetFileDto>>
{
    public async Task<List<GetFileDto>> Handle(GetFilteredSortedQuery request, CancellationToken cancellationToken)
    {
        var files = await unitOfWork.FilesRepo.GetFilteredSorted(request.Filter, cancellationToken);

        var filesDto = files.Select(f => new GetFileDto
        {
            Name = f.Name,
            Id = f.Id,
            ParentFolderId = f.ParentFolderId,
            CreationDate = f.CreationDate,
            SizeBytes = f.SizeBytes
        }).ToList();

        return filesDto;
    }
}