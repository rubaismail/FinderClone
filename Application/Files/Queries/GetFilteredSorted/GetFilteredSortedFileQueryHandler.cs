using Application.Dtos.Files;
using Application.Dtos.Folders;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Files.Queries.GetFilteredSorted;

public class GetFilteredSortedFileQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetFilteredSortedFileQuery, List<GetFileDto>>
{
    public async Task<List<GetFileDto>> Handle(GetFilteredSortedFileQuery request, CancellationToken cancellationToken)
    {
        // var requestDto = mapper.Map<DynamicFilterSortDto>(request);
        var files = await unitOfWork.FilesRepo.GetFilteredSorted(request.Filter, cancellationToken);

        // var filesDto = files.Select(f => new GetFileDto
        // {
        //     Name = f.Name,
        //     Id = f.Id,
        //     ParentFolderId = f.ParentFolderId,
        //     CreationDate = f.CreationDate,
        //     SizeBytes = f.SizeBytes
        // }).ToList();
        
        var filesDto = mapper.Map<List<GetFileDto>>(files);

        return filesDto;
    }
}