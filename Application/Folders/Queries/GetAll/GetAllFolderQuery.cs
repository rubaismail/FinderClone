using Application.Dtos.Folders;
using MediatR;

namespace Application.Folders.Queries.GetAll;

public class GetAllFolderQuery : IRequest<List<GetManyFoldersDto>>;