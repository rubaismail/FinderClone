namespace FinderClone.Dtos;

public class CreateFileDto
{
    public string Name { get; set; }
    public Guid ParentFolderId { get; set; }
    public String? RelativePath { get; set; }
}