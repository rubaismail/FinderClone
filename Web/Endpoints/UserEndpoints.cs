using Application.Dtos.Users;
using Application.Services.Interfaces;

namespace Web.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/register", async (RegisterUserDto registerDto, IUserService userService, CancellationToken cancellationToken) =>
        {
            var success = await userService.RegisterUserAsync(registerDto, cancellationToken);
            return success ? Results.Ok("User created") : Results.BadRequest("Username already taken");
        }).WithTags("Users");

        app.MapPost("/login", async (LoginUserDto loginDto, IUserService userService, CancellationToken cancellationToken) =>
        {
            var token = await userService.AuthenticateUserAsync(loginDto, cancellationToken);
            return token is null ? Results.Unauthorized() : Results.Ok(new { token });
        }).WithTags("Users");
    }
}

// logout endpoint