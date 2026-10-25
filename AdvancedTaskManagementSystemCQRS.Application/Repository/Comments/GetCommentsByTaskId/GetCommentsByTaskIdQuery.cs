using AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.GetCommentsByTaskId;

public record GetCommentsByTaskIdQuery(int TaskId) : IRequest<List<CommentDTO>>;