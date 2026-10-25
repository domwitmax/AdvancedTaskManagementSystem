using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Comments.DeleteComment;

public class DeleteCommentCommandHandler(IMediator mediator) : IRequestHandler<DeleteCommentCommand, bool>
{
    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new Repository.Comments.DeleteComment.DeleteCommentCommand(request.CommentId), cancellationToken);
    }
}
