using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeletePriority;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.DeletePriority
{
    public class DeletePriorityEntityCommandHandler(TaskManagerDbContext taskManagerDbContext) : IRequestHandler<DeletePriorityEntityCommand, bool>
    {
        public async Task<bool> Handle(DeletePriorityEntityCommand request, CancellationToken cancellationToken)
        {
            var priority = await taskManagerDbContext.Priorities.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if(priority == null) return false;

            var hasTasks = await taskManagerDbContext.Task.AnyAsync(t => t.PriorityId == request.Id, cancellationToken);
            if (hasTasks) return false;

            taskManagerDbContext.Priorities.Remove(priority);
            await taskManagerDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
