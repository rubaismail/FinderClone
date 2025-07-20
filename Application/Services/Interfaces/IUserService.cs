using Application.Dtos.Users;

namespace Application.Services.Interfaces;

public interface IUserService
{
    Task<bool> RegisterUserAsync(RegisterUserDto dto);
    Task<string?> AuthenticateUserAsync(LoginUserDto dto);
}