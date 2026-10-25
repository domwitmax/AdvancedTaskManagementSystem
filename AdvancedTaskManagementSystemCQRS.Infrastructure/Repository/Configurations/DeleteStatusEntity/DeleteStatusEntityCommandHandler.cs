using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeleteStatus;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.DeleteStatus
{
    public class DeleteStatusEntityCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<DeleteStatusEntityCommand, bool>
    {
        public async Task<bool> Handle(DeleteStatusEntityCommand request, CancellationToken cancellationToken)
        {
            var status = await dbContext.Status.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
            if (status == null) return false;

            var hasTasks = await dbContext.Task.AnyAsync(t => t.StatusId == request.Id, cancellationToken);
            if (hasTasks) return false;

            dbContext.Status.Remove(status);
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
