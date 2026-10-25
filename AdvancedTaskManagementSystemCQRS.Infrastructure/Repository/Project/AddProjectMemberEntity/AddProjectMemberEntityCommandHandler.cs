using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.AddProjectMemberEntity;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Project.AddProjectMemberEntity;

public class AddProjectMemberEntityCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<AddProjectMemberEntityCommand, bool>
{
    public async Task<bool> Handle(AddProjectMemberEntityCommand request, CancellationToken cancellationToken)
    {
        if (!await dbContext.Projects.AnyAsync(item => item.Id == request.ProjectId, cancellationToken))
            return false;

        var accountExists = await dbContext.Account.AnyAsync(
            item => item.Login == request.UserLogin && item.Active,
            cancellationToken);
        if (!accountExists)
            return false;

        if (await dbContext.ProjectMembers.AnyAsync(
            item => item.ProjectId == request.ProjectId && item.AccountLogin == request.UserLogin,
            cancellationToken))
            return false;

        dbContext.ProjectMembers.Add(new ProjectMemberEntity
        {
            ProjectId = request.ProjectId,
            AccountLogin = request.UserLogin,
            AddedAt = DateTime.UtcNow,
            AddedBy = request.AddedBy
        });

        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
