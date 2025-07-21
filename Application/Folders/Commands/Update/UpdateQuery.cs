using Application.Dtos.Folders;
using MediatR;

namespace Application.Folders.Commands.Update;

public class UpdateQuery : IRequest<bool>
{
    public UpdateFolderDto FolderDto { get; set; }
    public Guid Id { get; set; }
}