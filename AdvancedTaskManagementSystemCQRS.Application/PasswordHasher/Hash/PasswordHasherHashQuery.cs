using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.PasswordHasher.Hash;

public record PasswordHasherHashQuery(string Password) : IRequest<string>;