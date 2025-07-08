using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FinderClone.Models;
using Microsoft.IdentityModel.Tokens;

namespace FinderClone.Jwt;

public class JwtTokenGenerator (IConfiguration config) : IJwtTokenGenerator
{
    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, "admin")
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(config["Jwt:ExpiresInMinutes"]!)),
            signingCredentials: creds
        );
        var jwtString = new JwtSecurityTokenHandler().WriteToken(token);
        
        return jwtString;
    }
}