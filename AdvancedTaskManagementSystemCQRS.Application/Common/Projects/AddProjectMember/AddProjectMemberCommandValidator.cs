using FluentValidation;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.AddProjectMember;

public class AddProjectMemberCommandValidator : AbstractValidator<AddProjectMemberCommand>
{
    public AddProjectMemberCommandValidator()
    {
        RuleFor(item => item.ProjectId).GreaterThan(0);
        RuleFor(item => item.UserLogin).NotEmpty().MaximumLength(50);
        RuleFor(item => item.AddedBy).NotEmpty().MaximumLength(50);
    }
}
