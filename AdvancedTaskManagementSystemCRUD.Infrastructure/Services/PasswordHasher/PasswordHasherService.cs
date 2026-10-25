using AdvancedTaskManagementSystemCRUD.Application.Services.PasswordHasher;
using Microsoft.AspNetCore.Identity;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.Services.PasswordHasher;

public class PasswordHasherService : IPasswordHasher
{
    private readonly PasswordHasher<string> hasher = new PasswordHasher<string>();
    public string Hash(string password)
    {
        return hasher.HashPassword(null!, password);
    }

    public bool Verify(string password, string hash)
    {
        var result = hasher.VerifyHashedPassword(null!, hash, password);
        return result == PasswordVerificationResult.Success;
    }
}