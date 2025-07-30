using MediatR;

namespace Application.Files.Commands.Delete;

public class DeleteFileCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}