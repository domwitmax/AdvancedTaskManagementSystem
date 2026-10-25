using FluentValidation;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.UpdateStatus
{
    public class UpdateStatusCommandValidator : AbstractValidator<UpdateStatusCommand>
    {
        public UpdateStatusCommandValidator(IMediator mediator)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nazwa statusu jest wymagana.")
                .MaximumLength(100).WithMessage("Nazwa statusu może mieć maksymalnie 100 znaków.");
        }
    }
}
