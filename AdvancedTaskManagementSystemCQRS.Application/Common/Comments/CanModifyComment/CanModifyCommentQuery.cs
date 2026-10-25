using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Comments.CanModifyComment;

public record CanModifyCommentQuery(
    string CommentCreatedBy,
    string? CurrentUserLogin,
    bool IsAdministrator) : IRequest<bool>;
