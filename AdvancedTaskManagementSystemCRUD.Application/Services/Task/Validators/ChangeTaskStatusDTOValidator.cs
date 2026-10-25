using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;
using AdvancedTaskManagementSystemCRUD.Domain.Constants;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Task.Validators;

public sealed class ChangeTaskStatusDTOValidator : AbstractValidator<ChangeTaskStatusDTO>
{
    public ChangeTaskStatusDTOValidator(
        ITaskRepository taskRepository,
        IConfigurationRepository configurationRepository,
        IAccountRepository accountRepository,
        IProjectRepository projectRepository)
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Identyfikator zadania musi być większy od 0.");
        RuleFor(x => x.StatusId).GreaterThan(0).WithMessage("Identyfikator statusu zadania musi być większy od 0.");

        RuleFor(x => x)
            .MustAsync(async (command, cancellation) =>
            {
                var task = await taskRepository.GetTaskById(command.Id);
                if (task == null)
                    return true;
                return configurationRepository.GetStatusByTypeId(task.TypeId)
                    .Any(status => status.Id == command.StatusId);
            }).WithMessage("Status zadania nie należy do typu zadania.");

        RuleFor(x => x)
            .MustAsync(async (command, cancellation) =>
            {
                var task = await taskRepository.GetTaskById(command.Id);
                if (task == null)
                    return true;
                var actor = await accountRepository.GetAccount(command.ChangedBy);
                if (actor?.Active != true)
                    return false;
                return actor.Role?.Name == RoleNames.Administrator ||
                       projectRepository.IsProjectMember(task.ProjectId, command.ChangedBy);
            }).WithMessage("Użytkownik wykonujący operację nie ma dostępu do projektu zadania.");
    }
}
