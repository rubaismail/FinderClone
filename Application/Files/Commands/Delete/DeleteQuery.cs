using MediatR;

namespace Application.Files.Commands.Delete;

public class DeleteQuery : IRequest<bool>
{
    public Guid Id { get; set; }
}