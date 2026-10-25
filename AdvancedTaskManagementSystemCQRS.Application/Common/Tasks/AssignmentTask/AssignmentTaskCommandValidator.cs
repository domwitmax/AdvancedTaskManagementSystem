using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccount;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntityById;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.IsProjectMember;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetGroups;
using AdvancedTaskManagementSystemCQRS.Domain.Constants;
using FluentValidation;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.AssignmentTask;

public sealed class AssignmentTaskCommandValidator : AbstractValidator<AssignmentTaskCommand>
{
    public AssignmentTaskCommandValidator(IMediator mediator)
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Identyfikator zadania musi być większy od 0.");
        RuleFor(x => x.AssignedTo)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Login przypisanego użytkownika jest wymagany.")
            .MaximumLength(100).WithMessage("Login przypisanego użytkownika może mieć maksymalnie 100 znaków.")
            .MustAsync(async (command, assignedTo, cancellation) =>
            {
                var groups = await mediator.Send(new GetGroupsQuery(), cancellation);
                if (groups.Any(group => group.Equals(assignedTo, StringComparison.OrdinalIgnoreCase)))
                    return true;
                var account = await mediator.Send(new GetAccountEntityQuery(assignedTo), cancellation);
                if (account?.Active != true)
                    return false;
                var task = await mediator.Send(new GetTaskEntityByIdQuery(command.Id), cancellation);
                return task != null && await mediator.Send(new IsProjectMemberQuery(task.ProjectId, assignedTo), cancellation);
            }).WithMessage("Użytkownik musi być aktywnym członkiem projektu zadania albo istniejącą grupą.")
            .NotEmpty();

        RuleFor(x => x)
            .MustAsync(async (command, cancellation) =>
            {
                var task = await mediator.Send(new GetTaskEntityByIdQuery(command.Id), cancellation);
                if (task == null)
                    return true;
                var actor = await mediator.Send(new GetAccountEntityQuery(command.ChangedBy), cancellation);
                if (actor?.Active != true)
                    return false;
                return actor.RoleName == RoleNames.Administrator ||
                       await mediator.Send(new IsProjectMemberQuery(task.ProjectId, command.ChangedBy), cancellation);
            }).WithMessage("Użytkownik wykonujący operację nie ma dostępu do projektu zadania.");
    }
}
