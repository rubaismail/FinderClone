using Application.Dtos.Files;
using Application.Dtos.Folders;
using Application.Interfaces;
using MediatR;

namespace Application.Files.Queries.GetAll;

public class GetAllQueryHandler (IUnitOfWork unitOfWork) : IRequestHandler<GetAllQuery, List<GetFileDto>>
{
    public async Task<List<GetFileDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var files = await unitOfWork.FilesRepo.GetAll(cancellationToken);
        var filesDto = files.Select(f => new GetFileDto
        {
            Name = f.Name,
            Id = f.Id,
            ParentFolderId = f.ParentFolderId
        }).ToList();
        
        return filesDto;
    }
}