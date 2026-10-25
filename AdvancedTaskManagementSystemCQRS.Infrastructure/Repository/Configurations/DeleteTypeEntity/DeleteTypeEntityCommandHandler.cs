using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeleteTypeEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.DeleteTypeEntity
{
    public class DeleteTypeEntityCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<DeleteTypeEntityCommand, bool>
    {
        public async Task<bool> Handle(DeleteTypeEntityCommand request, CancellationToken cancellationToken)
        {
            var type = await dbContext.Type.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (type == null) return false;

            var hasStatuses = await dbContext.Status.AnyAsync(s => s.TypeId == request.Id, cancellationToken);
            var hasTasks = await dbContext.Task.AnyAsync(t => t.TypeId == request.Id, cancellationToken);
            if (hasStatuses || hasTasks) return false;

            dbContext.Type.Remove(type);
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
