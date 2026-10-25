using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DeleteTaskEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Tasks.DeleteTaskEntity
{
    public class DeleteTaskEntityCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<DeleteTaskEntityCommand, bool>
    {
        public async Task<bool> Handle(DeleteTaskEntityCommand request, CancellationToken cancellationToken)
        {
            var taskEntity = dbContext.Task.Find(request.Id);
            if (taskEntity == null)
            {
                return false;
            }
            dbContext.Task.Remove(taskEntity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
