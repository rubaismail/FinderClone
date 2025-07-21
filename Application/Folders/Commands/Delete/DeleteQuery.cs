using MediatR;

namespace Application.Folders.Commands.Delete;

public class DeleteQuery : IRequest<bool>
{
    public Guid Id { get; set; }
}