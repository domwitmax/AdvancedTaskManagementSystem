using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DeleteProjectMemberEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Project.DeleteProjectMemberEntity;

public class DeleteProjectMemberEntityCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<DeleteProjectMemberEntityCommand, bool>
{
    public async Task<bool> Handle(DeleteProjectMemberEntityCommand request, CancellationToken cancellationToken)
    {
        var member = await dbContext.ProjectMembers.FirstOrDefaultAsync(
            item => item.ProjectId == request.ProjectId && item.AccountLogin == request.UserLogin,
            cancellationToken);
        if (member == null)
            return false;

        if (await dbContext.Task.AnyAsync(
            item => item.ProjectId == request.ProjectId && item.AssignedTo == request.UserLogin,
            cancellationToken))
            return false;

        dbContext.ProjectMembers.Remove(member);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
