using Application.Dtos.Files;
using MediatR;

namespace Application.Files.Queries.GetById;

public class GetByIdQuery : IRequest<GetFileDto>
{
    public Guid Id { get; set; }
}