using AdvancedTaskManagementSystemCRUD.Application.Services.Account.DTOs;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Account.Validators;

public class ChangeRoleDTOValidator : AbstractValidator<ChangeRoleDTO>
{
    public ChangeRoleDTOValidator()
    {
        RuleFor(item => item.Login).NotEmpty().MaximumLength(50);
        RuleFor(item => item.RoleId).GreaterThan(0);
    }
}
