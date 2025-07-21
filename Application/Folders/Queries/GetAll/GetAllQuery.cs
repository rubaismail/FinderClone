using Application.Dtos.Folders;
using MediatR;

namespace Application.Folders.Queries.GetAll;

public class GetAllQuery : IRequest<List<GetManyFoldersDto>>;