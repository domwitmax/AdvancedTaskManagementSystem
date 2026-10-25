using FluentValidation;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Comments.DeleteComment;

public sealed class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(x => x.CommentId)
            .GreaterThan(0).WithMessage("Identyfikator komentarza musi być większy od 0.");
    }
}
