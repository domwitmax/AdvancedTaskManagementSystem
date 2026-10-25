using AdvancedTaskManagementSystemCQRS.Application.Interfaces;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Account.AddAccount;

public class AddAccountEntityCommand : IRequest, IDbCommand
{
    public string Login { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Active { get; set; }
}