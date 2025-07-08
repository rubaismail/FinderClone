using FinderClone.Dtos.Users;

namespace FinderClone.Services.Interfaces;

public interface IUserService
{
    Task<bool> RegisterUserAsync(RegisterUserDto dto);
    Task<string?> AuthenticateUserAsync(LoginUserDto dto);
}