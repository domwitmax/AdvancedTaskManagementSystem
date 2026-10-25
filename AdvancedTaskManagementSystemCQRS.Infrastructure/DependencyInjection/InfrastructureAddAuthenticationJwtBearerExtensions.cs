using AdvancedTaskManagementSystemCQRS.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.DependencyInjection;

public static class InfrastructureAddAuthenticationJwtBearerExtensions
{
    public static void AddAuthenticationJwtBearer(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        var settings = new AuthenticationSettings();
        configuration.GetSection("Authentication").Bind(settings);

        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = "Bearer";
            option.DefaultScheme = "Bearer";
            option.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = settings.JwtIssuer,
                ValidAudience = settings.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtKey)),
                NameClaimType = ClaimTypes.Name,
                RoleClaimType = ClaimTypes.Role
            };
        });
    }
}
