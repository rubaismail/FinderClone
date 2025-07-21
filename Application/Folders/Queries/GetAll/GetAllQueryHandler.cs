using Application.Dtos.Folders;
using Application.Interfaces;
using MediatR;

namespace Application.Folders.Queries.GetAll;

public class GetAllQueryHandler (IUnitOfWork unitOfWork) : IRequestHandler<GetAllQuery, List<GetManyFoldersDto>>
{
    public async Task<List<GetManyFoldersDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var folders = await unitOfWork.FoldersRepo.GetAll(cancellationToken);
        var foldersDto = folders.Select(f => new GetManyFoldersDto
        {
            Name = f.Name,
            Id = f.Id,
        }).ToList();

        return foldersDto;
    }
}