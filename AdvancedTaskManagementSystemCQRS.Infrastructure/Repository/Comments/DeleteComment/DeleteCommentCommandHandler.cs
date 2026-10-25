using AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.DeleteComment;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Comments.DeleteComment
{
    public class DeleteCommentCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<DeleteCommentCommand, bool>
    {
        public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == request.CommentId, cancellationToken);
            if (comment == null) return false;

            dbContext.Comments.Remove(comment);
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
