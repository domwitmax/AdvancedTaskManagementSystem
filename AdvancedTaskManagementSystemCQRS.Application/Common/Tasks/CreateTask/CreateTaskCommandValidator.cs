using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccount;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.GetProjectEntityById;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.IsProjectMember;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetGroupEntities;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetLabelEntities;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetPriorityEntities;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetStatusEntityByTypeId;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetTypeEntities;
using AdvancedTaskManagementSystemCQRS.Domain.Constants;
using FluentValidation;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.CreateTask;

public sealed class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator(IMediator mediator)
    {
        RuleFor(x => x.Id)
            .Equal(0).WithMessage("Nowe zadanie nie może zawierać istniejącego identyfikatora.");

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
            .MustAsync(async (typeId, cancellation) =>
            {
                var types = await mediator.Send(new GetTypeEntitiesQuery(), cancellation);
                return types.Any(item => item.Id == typeId);
            }).WithMessage("Nieprawidłowy identyfikator typu zadania.");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Identyfikator statusu zadania musi być większy od 0.");

        RuleFor(x => x)
            .MustAsync(async (command, cancellation) =>
            {
                var types = await mediator.Send(new GetTypeEntitiesQuery(), cancellation);
                var type = types.FirstOrDefault(item => item.Id == command.TypeId);
                if (type == null)
                    return false;
                var statuses = await mediator.Send(new GetStatusEntityByTypeIdQuery(command.TypeId), cancellation);
                return statuses.Any(status => status.Id == command.StatusId);
            }).WithMessage("Status zadania nie należy do typu zadania");

        RuleFor(x => x.PriorityId)
            .GreaterThan(0).WithMessage("Identyfikator priorytetu zadania musi być większy od 0.")
            .MustAsync(async (priorityId, cancellation) =>
            {
                var priorities = await mediator.Send(new GetPriorityEntitiesQuery(), cancellation);
                return priorities.Any(item => item.Id == priorityId);
            }).WithMessage("Nieprawidłowy identyfikator priorytetu zadania.");

        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("Identyfikator projektu musi być większy od 0.")
            .MustAsync(async (projectId, cancellation) =>
            {
                var project = await mediator.Send(new GetProjectEntityByIdQuery(projectId), cancellation);
                return project != null;
            }).WithMessage("Nieprawidłowy identyfikator projektu.");

        RuleFor(x => x.AssignedTo)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Login przypisanego użytkownika lub nazwa grupy jest wymagana.")
            .MaximumLength(100).WithMessage("Login przypisanego użytkownika może mieć maksymalnie 100 znaków.")
            .MustAsync(async (command, assignedTo, cancellation) =>
            {
                var groups = await mediator.Send(new GetGroupEntitiesQuery(), cancellation);
                if (groups.Any(group => group.Equals(assignedTo, StringComparison.OrdinalIgnoreCase)))
                    return true;

                var account = await mediator.Send(new GetAccountEntityQuery(assignedTo), cancellation);
                if (account?.Active != true)
                    return false;

                return await mediator.Send(new IsProjectMemberQuery(command.ProjectId, assignedTo), cancellation);
            }).WithMessage("Użytkownik musi być aktywnym członkiem wybranego projektu albo istniejącą grupą.");

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow.Date).WithMessage("Termin zadania musi być datą przyszłą.")
            .When(x => x.DueDate.HasValue);

        RuleFor(x => x.LabelIds)
            .MustAsync(async (labelIds, cancellation) =>
            {
                var distinctIds = labelIds?.Distinct().ToList() ?? new List<int>();
                if (distinctIds.Count == 0)
                    return true;
                var labels = await mediator.Send(new GetLabelEntitiesQuery(), cancellation);
                return distinctIds.All(id => labels.Any(label => label.Id == id));
            }).WithMessage("Co najmniej jedna z wybranych etykiet nie istnieje.");

        RuleFor(x => x)
            .MustAsync(async (command, cancellation) =>
            {
                var account = await mediator.Send(new GetAccountEntityQuery(command.CreatedBy), cancellation);
                if (account?.Active != true)
                    return false;
                if (account.RoleName == RoleNames.Administrator)
                    return true;
                return await mediator.Send(new IsProjectMemberQuery(command.ProjectId, command.CreatedBy), cancellation);
            }).WithMessage("Użytkownik tworzący zadanie nie ma dostępu do wybranego projektu.");
    }
}
