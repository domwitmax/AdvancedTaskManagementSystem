using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;
using AdvancedTaskManagementSystemCRUD.Domain.Constants;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Task.Validators;

public sealed class AssignmentTaskDTOValidator : AbstractValidator<AssignmentTaskDTO>
{
    public AssignmentTaskDTOValidator(
        IConfigurationRepository configurationRepository,
        IAccountRepository accountRepository,
        ITaskRepository taskRepository,
        IProjectRepository projectRepository)
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Identyfikator zadania musi być większy od 0.");

        RuleFor(x => x.AssignedTo)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Login przypisanego użytkownika jest wymagany.")
            .MaximumLength(100).WithMessage("Login przypisanego użytkownika może mieć maksymalnie 100 znaków.")
            .MustAsync(async (command, assignedTo, cancellation) =>
            {
                if (configurationRepository.GetGroups().Any(
                    group => group.Equals(assignedTo, StringComparison.OrdinalIgnoreCase)))
                    return true;

                var account = await accountRepository.GetAccount(assignedTo);
                if (account?.Active != true)
                    return false;
                var task = await taskRepository.GetTaskById(command.Id);
                return task != null && projectRepository.IsProjectMember(task.ProjectId, assignedTo);
            }).WithMessage("Użytkownik musi być aktywnym członkiem projektu zadania albo istniejącą grupą.");

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
