using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeleteGroupEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.DeleteGroupEntity
{
    public class DeleteGroupEntityCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<DeleteGroupEntityCommand, bool>
    {
        public async Task<bool> Handle(DeleteGroupEntityCommand request, CancellationToken cancellationToken)
        {
            var group = await dbContext.Groups.FirstOrDefaultAsync(g => g.Name == request.Name, cancellationToken);
            if(group == null) return false;

            var hasTasks = await dbContext.Task.AnyAsync(t => t.AssignedTo == group.Name, cancellationToken);
            if (hasTasks) return false;

            dbContext.Groups.Remove(group);
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
