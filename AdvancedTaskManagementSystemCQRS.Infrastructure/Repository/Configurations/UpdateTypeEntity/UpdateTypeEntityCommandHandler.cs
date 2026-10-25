using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdateTypeEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.UpdateTypeEntity
{
    public class UpdateTypeEntityCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<UpdateTypeEntityCommand, int?>
    {
        public async Task<int?> Handle(UpdateTypeEntityCommand request, CancellationToken cancellationToken)
        {
            var type = dbContext.Type.FirstOrDefault(t => t.Id == request.Id);
            if (type == null) return null;
            type.Name = request.Name;
            await dbContext.SaveChangesAsync(cancellationToken);
            return type.Id;
        }
    }
}
