using AdvancedTaskManagementSystemCQRS.Infrastructure.Configuration;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdvancedTaskManagementSystemCQRS.Application.Token.GenerateToken;

public class TokenGenerateQueryHandler(IOptions<AuthenticationSettings> settings) : IRequestHandler<TokenGenerateQuery, string>
{
    public Task<string> Handle(TokenGenerateQuery request, CancellationToken cancellationToken)
    {
        var user = request.User;

        var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Login),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.RoleName)
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
        return Task.FromResult(tokenHandler.WriteToken(token));
    }
}
