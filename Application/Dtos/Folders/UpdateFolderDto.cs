namespace Application.Dtos.Folders;

public class UpdateFolderDto
{
    public Guid? ParentFolderId { get; set; }
    public string? Name { get; set; }
    public List<Guid?> SubFoldersIds { get; set; }
    public List<Guid?> FilesIds { get; set; }
    public string? RelativePath { get; set; }
}