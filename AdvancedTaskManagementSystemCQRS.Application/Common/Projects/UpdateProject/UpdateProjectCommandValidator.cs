using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetProjectStatusEntities;
using FluentValidation;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.UpdateProject;

public sealed class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator(IMediator mediator)
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Identyfikator projektu musi być większy od 0.");

        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Nazwa projektu jest wymagana.")
            .MaximumLength(100).WithMessage("Nazwa projektu może mieć maksymalnie 100 znaków.");

        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Opis projektu jest wymagany.")
            .MaximumLength(500).WithMessage("Opis projektu może mieć maksymalnie 500 znaków.");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("Identyfikator statusu projektu musi być większy od 0.")
            .MustAsync(async (statusId, cancellationToken) =>
            {
                var statuses = await mediator.Send(new GetProjectStatusEntitiesQuery(), cancellationToken);
                return statuses.Any(status => status.Id == statusId);
            }).WithMessage("Nieprawidłowy identyfikator statusu projektu.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Data rozpoczęcia projektu jest wymagana.");

        RuleFor(x => x.EndDate)
            .Must((project, endDate) => !endDate.HasValue || endDate.Value >= project.StartDate)
            .WithMessage("Data zakończenia projektu nie może być wcześniejsza niż data rozpoczęcia.");
    }
}
