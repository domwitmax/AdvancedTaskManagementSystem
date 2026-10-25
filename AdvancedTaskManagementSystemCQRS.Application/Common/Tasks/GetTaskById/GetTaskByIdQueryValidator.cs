using FluentValidation;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetTaskById;

public sealed class GetTaskByIdQueryValidator : AbstractValidator<GetTaskByIdQuery>
{
    public GetTaskByIdQueryValidator()
    {
        RuleFor(x => x.TaskId)
            .GreaterThan(0).WithMessage("Identyfikator zadania musi być większy od 0.");
    }
}
