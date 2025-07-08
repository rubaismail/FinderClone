namespace FinderClone.Dtos;

public class GetFileDto
{
    public Guid Id { get; set; }
    public String Name { get; set; }
    public Guid ParentFolderId { get; set; }
}