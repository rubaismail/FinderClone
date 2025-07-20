// using Web.Data;
// using Web.Jwt;
// using Web.Models;
// using Web.Repositories.Interfaces;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
//
// namespace Web.Repositories;
//
// public class UserRepository 
//     (AppDbContext dbContext, IPasswordHasher<User> hasher, IJwtTokenGenerator jwt): IUserRepository
// {
//     public async Task<bool> RegisterUser(User thisUser)
//     {
//         if (await dbContext.Users.AnyAsync(u => u.Username == thisUser.Username))
//             return false;
//
//         //var newUser = new User { Username = thisUser.Username };
//         thisUser.PasswordHash = hasher.HashPassword(thisUser, thisUser.PasswordHash);
//
//         dbContext.Users.Add(thisUser);
//         await dbContext.SaveChangesAsync();
//
//         return true;
//     }
//
//     public async Task<string?> AuthenticateUser(User thisUser)
//     {
//         var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Username == thisUser.Username);
//         if (user == null) return null;
//
//         var result = hasher.VerifyHashedPassword(user, user.PasswordHash, thisUser.PasswordHash);
//         if (result == PasswordVerificationResult.Failed) return null;
//
//         return jwt.GenerateToken(user);
//     }
// }