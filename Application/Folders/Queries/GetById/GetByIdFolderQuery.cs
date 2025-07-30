using Application.Dtos.Folders;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Folders.Queries.GetById;

public class GetByIdFolderQuery : IRequest<GetFolderDto>
{
    public Guid Id { get; set; } 
}