using Application.Dtos.Files;
using MediatR;

namespace Application.Files.Queries.GetById;

public class GetByIdFileQuery : IRequest<GetFileDto>
{
    public Guid Id { get; set; }
}