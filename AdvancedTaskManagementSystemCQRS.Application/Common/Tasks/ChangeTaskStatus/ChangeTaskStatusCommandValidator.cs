using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccount;
using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetStatusByTypeId;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetTaskById;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.IsProjectMember;
using AdvancedTaskManagementSystemCQRS.Domain.Constants;
using FluentValidation;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.ChangeTaskStatus;

public sealed class ChangeTaskStatusCommandValidator : AbstractValidator<ChangeTaskStatusCommand>
{
    public ChangeTaskStatusCommandValidator(IMediator mediator)
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Identyfikator zadania musi być większy od 0.");
        RuleFor(x => x.StatusId).GreaterThan(0).WithMessage("Identyfikator statusu zadania musi być większy od 0.");

        RuleFor(x => x)
            .MustAsync(async (command, cancellation) =>
            {
                var task = await mediator.Send(new GetTaskByIdQuery(command.Id), cancellation);
                if (task == null)
                    return true;
                var statuses = await mediator.Send(new GetStatusByTypeIdQuery(task.TypeId), cancellation);
                return statuses.Any(status => status.Id == command.StatusId);
            }).WithMessage("Status zadania nie należy do typu zadania.");

        RuleFor(x => x)
            .MustAsync(async (command, cancellation) =>
            {
                var task = await mediator.Send(new GetTaskByIdQuery(command.Id), cancellation);
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
