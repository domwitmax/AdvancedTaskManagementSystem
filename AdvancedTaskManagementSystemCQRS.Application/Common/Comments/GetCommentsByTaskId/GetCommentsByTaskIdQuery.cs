using AdvancedTaskManagementSystemCQRS.Application.Common.Comments.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Comments.GetCommentsByTaskId;

public record GetCommentsByTaskIdQuery(int TaskId) : IRequest<List<CommentDTO>>;
