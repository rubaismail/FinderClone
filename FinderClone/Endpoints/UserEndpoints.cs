using FinderClone.Dtos.Users;
using FinderClone.Services.Interfaces;

namespace FinderClone.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/register", async (RegisterUserDto registerDto, IUserService userService) =>
        {
            var success = await userService.RegisterUserAsync(registerDto);
            return success ? Results.Ok("User created") : Results.BadRequest("Username already taken");
        }).WithTags("Users");

        app.MapPost("/login", async (LoginUserDto loginDto, IUserService userService) =>
        {
            var token = await userService.AuthenticateUserAsync(loginDto);
            return token is null ? Results.Unauthorized() : Results.Ok(new { token });
        }).WithTags("Users");
    }
}

// logout endpoint