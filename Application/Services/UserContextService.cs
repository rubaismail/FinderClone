using System.Security.Claims;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class UserContextService(IHttpContextAccessor httpContextAccessor) : IUserContextService
{
    public Guid? GetCurrentUserId()
    {
        var idString = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        Guid id = Guid.Parse(idString);

        return id;
    }
}