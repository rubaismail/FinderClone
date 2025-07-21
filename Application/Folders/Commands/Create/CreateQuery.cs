using Application.Dtos.Folders;
using Domain.Entities;
using MediatR;

namespace Application.Folders.Commands.CreateFolder;

public class CreateQuery :IRequest<Folder>
{
    public CreateFolderDto FolderDto { get; set; }
}