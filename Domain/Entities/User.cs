namespace Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    
    //[NotMapped]
    //public override string Name
    //{
       // get => Username;
       // set => Username = value;
    //}
    public string PasswordHash { get; set; }

    public string Role { get; set; } = "read";
    
}