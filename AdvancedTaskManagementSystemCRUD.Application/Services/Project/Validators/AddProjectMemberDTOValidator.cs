using AdvancedTaskManagementSystemCRUD.Application.Services.Project.DTOs;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Project.Validators;

public class AddProjectMemberDTOValidator : AbstractValidator<AddProjectMemberDTO>
{
    public AddProjectMemberDTOValidator()
    {
        RuleFor(item => item.ProjectId).GreaterThan(0);
        RuleFor(item => item.UserLogin).NotEmpty().MaximumLength(50);
        RuleFor(item => item.AddedBy).NotEmpty().MaximumLength(50);
    }
}
