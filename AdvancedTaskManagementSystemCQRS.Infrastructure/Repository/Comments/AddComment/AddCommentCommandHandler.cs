using AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.AddComment;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Comments.AddComment
{
    public class AddCommentCommandHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<AddCommentCommand, int?>
    {
        public async Task<int?> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var taskExists = await dbContext.Task.AnyAsync(item => item.Id == request.TaskId, cancellationToken);
            if (!taskExists)
                return null;

            var comment = mapper.Map<CommentEntity>(request);
            comment.CreatedAt = DateTime.UtcNow;
            dbContext.Comments.Add(comment);
            await dbContext.SaveChangesAsync(cancellationToken);
            return comment.Id;
        }
    }
}
