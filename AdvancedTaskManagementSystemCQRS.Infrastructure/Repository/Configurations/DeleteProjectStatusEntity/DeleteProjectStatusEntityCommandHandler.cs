using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeleteProjectStatusEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.DeleteProjectStatusEntity;

public class DeleteProjectStatusEntityCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<DeleteProjectStatusEntityCommand, bool>
{
    public async Task<bool> Handle(DeleteProjectStatusEntityCommand request, CancellationToken cancellationToken)
    {
        var status = await dbContext.ProjectStatuses.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
        if (status == null) return false;

        var hasProjects = await dbContext.Projects.AnyAsync(p => p.StatusId == request.Id, cancellationToken);
        if (hasProjects) return false;

        dbContext.ProjectStatuses.Remove(status);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
