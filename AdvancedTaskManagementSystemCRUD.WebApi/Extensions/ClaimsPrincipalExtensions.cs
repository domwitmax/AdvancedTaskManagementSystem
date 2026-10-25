using System.Security.Claims;

namespace AdvancedTaskManagementSystemCRUD.WebApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetLogin(this ClaimsPrincipal user)
    {
        return user.Identity?.Name
            ?? user.FindFirstValue(ClaimTypes.Name)
            ?? user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? user.FindFirstValue("name")
            ?? user.FindFirstValue("unique_name")
            ?? user.FindFirstValue("sub");
    }
}
