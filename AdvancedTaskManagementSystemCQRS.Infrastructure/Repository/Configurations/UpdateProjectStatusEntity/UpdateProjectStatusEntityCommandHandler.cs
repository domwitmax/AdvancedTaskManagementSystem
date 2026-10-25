using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdateProjectStatusEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.UpdateProjectStatusEntity;

public class UpdateProjectStatusEntityCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<UpdateProjectStatusEntityCommand, int?>
{
    public async Task<int?> Handle(UpdateProjectStatusEntityCommand request, CancellationToken cancellationToken)
    {
        var status = await dbContext.ProjectStatuses.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (status == null) return null;

        status.Name = request.Name;
        await dbContext.SaveChangesAsync(cancellationToken);
        return status.Id;
    }
}
