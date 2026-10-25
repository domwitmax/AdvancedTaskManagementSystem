using AdvancedTaskManagementSystemCQRS.Application.PasswordHasher.Verify;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.PasswordHasher.Verify;

public class PasswordHasherVerifyQueryHandler : IRequestHandler<PasswordHasherVerifyQuery, bool>
{
    public Task<bool> Handle(PasswordHasherVerifyQuery request, CancellationToken cancellationToken)
    {
        var hasher = new PasswordHasher<string>();
        var result = hasher.VerifyHashedPassword(null, request.Hash, request.Password);
        return Task.FromResult(result == PasswordVerificationResult.Success);
    }
}
