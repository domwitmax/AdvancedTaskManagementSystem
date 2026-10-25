using AdvancedTaskManagementSystemCQRS.Application.PasswordHasher.Hash;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.PasswordHasher.Hash;

public class PasswordHasherHashQueryHandler : IRequestHandler<PasswordHasherHashQuery, string>
{
    public Task<string> Handle(PasswordHasherHashQuery request, CancellationToken cancellationToken)
    {
        var hasher = new PasswordHasher<string>();
        return Task.FromResult(hasher.HashPassword(null!, request.Password));
    }
}
