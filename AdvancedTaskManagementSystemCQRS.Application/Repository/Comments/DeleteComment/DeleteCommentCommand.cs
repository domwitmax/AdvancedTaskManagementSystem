using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.DeleteComment;

public record DeleteCommentCommand(int CommentId) : IRequest<bool>;
