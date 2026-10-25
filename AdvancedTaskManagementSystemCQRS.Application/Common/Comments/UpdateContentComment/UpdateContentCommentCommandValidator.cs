using FluentValidation;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Comments.UpdateContentComment;

public sealed class UpdateContentCommentCommandValidator : AbstractValidator<UpdateContentCommentCommand>
{
    public UpdateContentCommentCommandValidator()
    {
        RuleFor(x => x.CommentId)
            .GreaterThan(0).WithMessage("Identyfikator komentarza musi być większy od 0.");

        RuleFor(x => x.Content)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Treść komentarza jest wymagana.")
            .MaximumLength(1000).WithMessage("Treść komentarza może mieć maksymalnie 1000 znaków.");
    }
}
