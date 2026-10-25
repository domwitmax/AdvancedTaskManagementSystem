using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.PasswordHasher.Verify;

public record PasswordHasherVerifyQuery(string Password, string Hash) : IRequest<bool>;