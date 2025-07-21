using Application.Dtos.Folders;
using MediatR;

namespace Application.Folders.Queries.GetById;

public class GetByIdQuery : IRequest<GetFolderDto>
{
    public Guid Id { get; set; } 
}