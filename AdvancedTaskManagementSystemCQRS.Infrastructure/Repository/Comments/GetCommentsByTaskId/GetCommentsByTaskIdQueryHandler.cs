using AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.GetCommentsByTaskId;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Comments.GetCommentsByTaskId
{
    public class GetCommentsByTaskIdQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetCommentsByTaskIdQuery, List<CommentDTO>>
    {
        public async Task<List<CommentDTO>> Handle(GetCommentsByTaskIdQuery request, CancellationToken cancellationToken)
        {
            var comments = await dbContext.Comments
                .Where(c => c.TaskId == request.TaskId)
                .OrderBy(c => c.CreatedAt)
                .ThenBy(c => c.Id)
                .ToListAsync(cancellationToken);
            return mapper.Map<List<CommentDTO>>(comments);
        }
    }
}
