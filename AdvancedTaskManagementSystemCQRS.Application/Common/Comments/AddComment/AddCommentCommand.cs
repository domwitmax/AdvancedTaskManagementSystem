using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Comments.AddComment;

public record AddCommentCommand(int TaskId, string Content, string CreatedBy) : IRequest<int?>;
