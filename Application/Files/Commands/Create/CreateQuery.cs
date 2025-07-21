using Application.Dtos.Files;
using Domain.Entities;
using MediatR;

namespace Application.Files.Commands.Create;

public class CreateQuery : IRequest<StoredFile>
{
    public string Name { get; set; }
    public Guid ParentFolderId { get; set; }
    public String? RelativePath { get; set; }
}