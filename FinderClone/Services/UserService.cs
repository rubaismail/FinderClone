using FinderClone.Dtos.Users;
using FinderClone.Models;
using FinderClone.Repositories.Interfaces;
using FinderClone.Services.Interfaces;

namespace FinderClone.Services;

public class UserService (IUserRepository userRepository) : IUserService
{
        
    public async Task<bool> RegisterUserAsync(RegisterUserDto registerDto)
    {
        var newUser = new User
        {
            Username = registerDto.Username,
            PasswordHash = registerDto.Password
        };
        var success = await userRepository.RegisterUser(newUser);
        
        return success;
    }

    public async Task<string?> AuthenticateUserAsync(LoginUserDto loginDto)
    {
        var user = new User
        {
            Username = loginDto.Username,
            PasswordHash = loginDto.Password
        };
        var token = await userRepository.AuthenticateUser(user);
        
        return token;
    }
}