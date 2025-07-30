using Application.Dtos.Files;
using MediatR;

namespace Application.Files.Queries.GetAll;

public class GetAllQuery : IRequest<List<GetFileDto>>;