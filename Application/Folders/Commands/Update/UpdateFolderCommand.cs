using Application.Dtos.Folders;
using MediatR;

namespace Application.Folders.Commands.Update;

public class UpdateFolderCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    
    public Guid? ParentFolderId { get; set; }
    public string? Name { get; set; }
    public List<Guid?> SubFoldersIds { get; set; }
    public List<Guid?> FilesIds { get; set; }
    public string? RelativePath { get; set; }
}