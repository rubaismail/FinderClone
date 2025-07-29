using Application.Dtos.Files;
using Application.Dtos.Folders;
using Application.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Files.Queries.GetById;

public class GetByIdFileQueryHandler (IUnitOfWork unitOfWork, IMapper mapper) 
    : IRequestHandler<GetByIdFileQuery, GetFileDto>
{
    public async Task<GetFileDto> Handle(GetByIdFileQuery request, CancellationToken cancellationToken)
    {
        var file = await unitOfWork.FilesRepo.GetById(request.Id, cancellationToken);
        if (file == null) return null;
        
        // var fileDto = new GetFileDto
        // {
        //     Name = file.Name,
        //     Id = file.Id,
        //     ParentFolderId = file.ParentFolderId,
        //     CreationDate = file.CreationDate,
        //     SizeBytes = file.SizeBytes
        // };
        
        var fileDto = mapper.Map<GetFileDto>(file);
        
        return fileDto;
    }
}