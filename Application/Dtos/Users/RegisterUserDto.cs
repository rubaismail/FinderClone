namespace Application.Dtos.Users;

public class RegisterUserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    
    public string? Role { get; set; }
}