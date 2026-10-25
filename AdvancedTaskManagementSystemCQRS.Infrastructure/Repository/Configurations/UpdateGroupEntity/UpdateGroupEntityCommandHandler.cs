using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdateGroupEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.UpdateGroupEntity
{
    public class UpdateGroupEntityCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<UpdateGroupEntityCommand, int?>
    {
        public async Task<int?> Handle(UpdateGroupEntityCommand request, CancellationToken cancellationToken)
        {
            var group = dbContext.Groups.FirstOrDefault(g => g.Name == request.oldName);
            if (group == null) return null;
            group.Name = request.newName;
            await dbContext.SaveChangesAsync(cancellationToken);
            return group.Id;
        }
    }
}
