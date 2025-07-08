using System.ComponentModel.DataAnnotations;
using FinderClone.Dtos;

namespace FinderClone.Models;

public class Folder
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(15)]
    public required string Name { get; set; } 
    public Guid? ParentFolderId { get; set; }
    public Folder? ParentFolder { get; set; }
    public List<Folder>? SubFolders { get; set; }
    public List<StoredFile>? Files { get; set; }
    public string? RelativePath { get; set; }
}