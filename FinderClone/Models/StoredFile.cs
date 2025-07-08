using System.ComponentModel.DataAnnotations;

namespace FinderClone.Models;

public class StoredFile
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(15)]
    public required string Name { get; set; }
    public Guid ParentFolderId { get; set; }
    public Folder? ParentFolder { get; set; }
    public string? RelativePath { get; set; }
}