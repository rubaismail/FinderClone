using Application.Dtos.Files;
using MediatR;

namespace Application.Files.Commands.Update;

public class UpdateQuery : IRequest<bool>
{
    public Guid Id { get; set; }
    public UpdateFileDto FileDto { get; set; }
}