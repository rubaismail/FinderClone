namespace Domain.Entities.Audit;

public interface IAuditable
{
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