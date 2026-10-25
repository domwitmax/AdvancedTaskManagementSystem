using FluentValidation;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.ChangeRole;

public class ChangeRoleCommandValidator : AbstractValidator<ChangeRoleCommand>
{
    public ChangeRoleCommandValidator()
    {
        RuleFor(item => item.Login).NotEmpty().MaximumLength(50);
        RuleFor(item => item.RoleId).GreaterThan(0);
    }
}
