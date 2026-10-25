using AdvancedTaskManagementSystemCQRS.Application.Interfaces;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Account.UpdateAccount;

public class UpdateAccountEntityCommand : IRequest, IDbCommand
{
    public string Login { get; set; } = null!;
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public DateTime? ChangePasswordAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? Active { get; set; }
}
