using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdatePriorityEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.UpdatePriorityEntity
{
    public class UpdatePriorityEntityCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<UpdatePriorityEntityCommand, int?>
    {
        public async Task<int?> Handle(UpdatePriorityEntityCommand request, CancellationToken cancellationToken)
        {
            var priority = dbContext.Priorities.FirstOrDefault(p => p.Id == request.Id);
            if (priority == null) return null;
            priority.Name = request.Name;
            await dbContext.SaveChangesAsync(cancellationToken);
            return priority.Id;
        }
    }
}
