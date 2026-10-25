using AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.GetCommentById;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Comments.GetCommentById;

public class GetCommentByIdQueryHandler(TaskManagerDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetCommentByIdQuery, CommentDTO?>
{
    public async Task<CommentDTO?> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var comment = await dbContext.Comments
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == request.CommentId, cancellationToken);
        return mapper.Map<CommentDTO?>(comment);
    }
}
