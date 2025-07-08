using FinderClone.Models;

namespace FinderClone.Jwt;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}