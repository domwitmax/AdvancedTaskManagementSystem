using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Comments.CanModifyComment;

public class CanModifyCommentQueryHandler : IRequestHandler<CanModifyCommentQuery, bool>
{
    public Task<bool> Handle(CanModifyCommentQuery request, CancellationToken cancellationToken)
    {
        var canModify = request.IsAdministrator ||
            (!string.IsNullOrWhiteSpace(request.CurrentUserLogin) &&
             request.CurrentUserLogin.Equals(
                 request.CommentCreatedBy,
                 StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(canModify);
    }
}
