using Application.Dtos.Folders;
using Application.Dtos.Users;
using Application.Interfaces;
using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserService (IUnitOfWork unitOfWork, IPasswordHasher<User> hasher, IJwtTokenGenerator jwt) : IUserService
{
    public async Task<bool> RegisterUserAsync(RegisterUserDto registerDto, CancellationToken cancellationToken)
    {
        var exists = await unitOfWork.UsersRepo.GetFilteredSorted(new DynamicFilterSortDto
        {
            FilterBy = "Username",
            Operation = FilterOperation.Equal,
            Value = registerDto.Username
        }, cancellationToken);
        if (exists.Count != 0)
            return false;
        
        var user = new User
        {
            Username = registerDto.Username,
            PasswordHash = registerDto.Password, 
            Role = string.IsNullOrWhiteSpace(registerDto.Role) ? "read" : registerDto.Role
        };

        user.PasswordHash = hasher.HashPassword(user, user.PasswordHash);
        await unitOfWork.UsersRepo.Add(user, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<string?> AuthenticateUserAsync(LoginUserDto loginDto, CancellationToken cancellationToken)
    {
        var exists = await unitOfWork.UsersRepo.GetFilteredSorted(new DynamicFilterSortDto
        {
            FilterBy = "Username",
            Operation = FilterOperation.Equal,
            Value = loginDto.Username
        }, cancellationToken);
        var user = exists.SingleOrDefault();
        if (user == null)
            return null;
        
        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
        if (result == PasswordVerificationResult.Failed) return null;
        
        return jwt.GenerateToken(user);
    }
}