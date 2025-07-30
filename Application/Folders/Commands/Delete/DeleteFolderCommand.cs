using MediatR;

namespace Application.Folders.Commands.Delete;

public class DeleteFolderCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}