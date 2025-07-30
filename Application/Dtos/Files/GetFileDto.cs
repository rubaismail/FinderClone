namespace Application.Dtos.Files;

public class GetFileDto
{
    public Guid Id { get; set; }
    public String Name { get; set; }
    public Guid ParentFolderId { get; set; }
    public long? SizeBytes { get; set; }
    public DateTime CreationDate { get; set; }
}