using FluentValidation;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Comments.AddComment;

public sealed class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
{
    public AddCommentCommandValidator()
    {
        RuleFor(x => x.TaskId)
            .GreaterThan(0).WithMessage("Identyfikator zadania musi być większy od 0.");

        RuleFor(x => x.Content)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Treść komentarza jest wymagana.")
            .MaximumLength(1000).WithMessage("Treść komentarza może mieć maksymalnie 1000 znaków.");
    }
}
