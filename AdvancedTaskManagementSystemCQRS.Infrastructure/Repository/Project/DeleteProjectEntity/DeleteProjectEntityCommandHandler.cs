using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DeleteProjectEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Project.DeleteProjectEntity;

public class DeleteProjectEntityCommandHandler(TaskManagerDbContext dbContext)
    : IRequestHandler<DeleteProjectEntityCommand, int>
{
    public async Task<int> Handle(DeleteProjectEntityCommand request, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects.FirstOrDefaultAsync(
            item => item.Id == request.ProjectId,
            cancellationToken);
        if (project == null)
            return 0;
        if (await dbContext.Task.AnyAsync(item => item.ProjectId == request.ProjectId, cancellationToken))
            return -1;

        dbContext.Projects.Remove(project);
        await dbContext.SaveChangesAsync(cancellationToken);
        return 1;
    }
}
