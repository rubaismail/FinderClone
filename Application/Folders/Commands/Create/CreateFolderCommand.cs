using Application.Dtos.Folders;
using Domain.Entities;
using MediatR;

namespace Application.Folders.Commands.CreateFolder;

public class CreateFolderCommand :IRequest<Folder>
{
    //public CreateFolderDto FolderDto { get; set; } = null!;
    public string Name { get; set; }
    public Guid ParentFolderId { get; set; }
    public String? RelativePath { get; set; }
}