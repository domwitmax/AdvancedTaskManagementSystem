using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdateStatusEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.UpdateStatusEntity
{
    public class UpdateStatusEntityCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<UpdateStatusEntityCommand, int?>
    {
        public async Task<int?> Handle(UpdateStatusEntityCommand request, CancellationToken cancellationToken)
        {
            var status = dbContext.Status.FirstOrDefault(s => s.Id == request.Id);
            if (status == null) return null;
            status.Name = request.Name;
            status.IsFinished = request.IsFinished;
            await dbContext.SaveChangesAsync(cancellationToken);
            return status.Id;
        }
    }
}
