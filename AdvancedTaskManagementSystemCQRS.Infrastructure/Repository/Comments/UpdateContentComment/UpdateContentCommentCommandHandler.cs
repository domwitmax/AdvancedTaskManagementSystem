using AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.UpdateContentComment;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Comments.UpdateContentComment
{
    public class UpdateContentCommentCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<UpdateContentCommentCommand, int?>
    {
        public async Task<int?> Handle(UpdateContentCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == request.CommentId, cancellationToken);
            if (comment == null) return null;

            comment.Content = request.Content;
            await dbContext.SaveChangesAsync(cancellationToken);
            return comment.Id;
        }
    }
}
