namespace Application.Dtos.Files;

public class CreateFileDto
{
    public string Name { get; set; }
    public Guid ParentFolderId { get; set; }
    public String? RelativePath { get; set; }
}