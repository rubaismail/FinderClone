namespace Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    
    // [Required]
    // [MaxLength(15)]
    // public virtual string Name { get; set; } 
}

//, IAuditable  // Add Auditing properties through an interface,
//  and fill these properties on each create/update/delete operation