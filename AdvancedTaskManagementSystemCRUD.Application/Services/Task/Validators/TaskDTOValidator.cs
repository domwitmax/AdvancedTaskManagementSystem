using AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Domain.Constants;
using FluentValidation;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Task.Validators;

public sealed class TaskDTOValidator : AbstractValidator<TaskDTO>
{
    public TaskDTOValidator(
        IConfigurationRepository configurationRepository,
        IAccountRepository accountRepository,
        IProjectRepository projectRepository)
    {
        RuleFor(x => x.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Tytuł zadania jest wymagany.")
            .MaximumLength(200).WithMessage("Tytuł zadania może mieć maksymalnie 200 znaków.");

        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Opis zadania jest wymagany.")
            .MaximumLength(2000).WithMessage("Opis zadania może mieć maksymalnie 2000 znaków.");

        RuleFor(x => x.TypeId)
            .GreaterThan(0).WithMessage("Identyfikator typu zadania musi być większy od 0.")
            .Must(typeId => configurationRepository.GetTypes().Any(item => item.Id == typeId))
            .WithMessage("Nieprawidłowy identyfikator typu zadania.");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Identyfikator statusu zadania musi być większy od 0.");

        RuleFor(x => x)
            .Must(command => configurationRepository.GetStatusByTypeId(command.TypeId)
                .Any(status => status.Id == command.StatusId))
            .WithMessage("Status zadania nie należy do typu zadania.");

        RuleFor(x => x.PriorityId)
            .GreaterThan(0).WithMessage("Identyfikator priorytetu zadania musi być większy od 0.")
            .Must(priorityId => configurationRepository.GetPriorities().Any(item => item.Id == priorityId))
            .WithMessage("Nieprawidłowy identyfikator priorytetu zadania.");

        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("Identyfikator projektu musi być większy od 0.")
            .Must(projectId => projectRepository.GetProjectByIdAsync(projectId) != null)
            .WithMessage("Nieprawidłowy identyfikator projektu.");

        RuleFor(x => x.AssignedTo)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Login przypisanego użytkownika lub nazwa grupy jest wymagana.")
            .MaximumLength(100)
            .WithMessage("Login przypisanego użytkownika może mieć maksymalnie 100 znaków.")
            .MustAsync(async (task, assignedTo, cancellation) =>
            {
                if (configurationRepository.GetGroups().Any(
                    group => group.Equals(assignedTo, StringComparison.OrdinalIgnoreCase)))
                    return true;

                var account = await accountRepository.GetAccount(assignedTo);
                return account?.Active == true && projectRepository.IsProjectMember(task.ProjectId, assignedTo);
            }).WithMessage("Użytkownik musi być aktywnym członkiem wybranego projektu albo istniejącą grupą.");

        RuleFor(x => x.LabelIds)
            .Must(labelIds =>
            {
                var ids = labelIds?.Distinct().ToList() ?? new List<int>();
                var labels = configurationRepository.GetLabels();
                return ids.All(id => labels.Any(label => label.Id == id));
            }).WithMessage("Co najmniej jedna z wybranych etykiet nie istnieje.");

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow.Date).WithMessage("Termin zadania musi być datą przyszłą.")
            .When(x => x.Id == 0 && x.DueDate.HasValue);

        RuleFor(x => x)
            .MustAsync(async (task, cancellation) =>
            {
                var actorLogin = task.Id == 0 ? task.CreatedBy : task.ChangedBy;
                var account = await accountRepository.GetAccount(actorLogin);
                if (account?.Active != true)
                    return false;
                if (account.Role?.Name == RoleNames.Administrator)
                    return true;
                return projectRepository.IsProjectMember(task.ProjectId, actorLogin);
            }).WithMessage("Użytkownik wykonujący operację nie ma dostępu do wybranego projektu.");
    }
}
