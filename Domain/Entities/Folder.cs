using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Audit;

namespace Domain.Entities;

public class Folder : BaseEntity, IAuditable
{
    [Required]
    [MaxLength(15)]
    public required string Name { get; set; } 
    public Guid? ParentFolderId { get; set; }
    public Folder? ParentFolder { get; set; }
    public List<Folder>? SubFolders { get; set; }
    public List<StoredFile>? Files { get; set; }
    public string? RelativePath { get; set; }
    
    [Column(TypeName = "date")]
    public DateTime CreationDate { get; set; }

    public Guid? CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? UpdatedById { get; set; }
    public DateTime UpdatedOn { get; set; }
    public User? UpdatedBy { get; set; }
    public Guid? DeletedById { get; set; }
    public DateTime DeletedOn { get; set; }
    public User? DeletedBy { get; set; }
}