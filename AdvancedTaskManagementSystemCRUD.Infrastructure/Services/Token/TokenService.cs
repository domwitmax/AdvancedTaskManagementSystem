using AdvancedTaskManagementSystemCRUD.Application.Services.Token;
using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using AdvancedTaskManagementSystemCRUD.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.Services.Token;

public class TokenService(
    IOptions<AuthenticationSettings> settings
    ) : ITokenService
{
    public string GenerateToken(AccountEntity user)
    {
        var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Login),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Value.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(settings.Value.JwtExpireDays);

        var token = new JwtSecurityToken(settings.Value.JwtIssuer,
            settings.Value.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}
