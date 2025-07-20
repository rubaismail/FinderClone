namespace Application.Dtos.Files;

public class UpdateFileDto
{
    public string Name { get; set; }
    public Guid ParentFolderId { get; set; }
    public string? RelativePath { get; set; }
    public long? SizeBytes { get; set; }
}