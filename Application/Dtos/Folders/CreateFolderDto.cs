namespace Application.Dtos.Folders;

public class CreateFolderDto
{
    public string Name { get; set; }
    public Guid ParentFolderId { get; set; }
    public String? RelativePath { get; set; }
}