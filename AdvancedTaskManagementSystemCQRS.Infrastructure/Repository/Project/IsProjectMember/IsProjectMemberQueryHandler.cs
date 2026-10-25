using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.IsProjectMember;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Project.IsProjectMember;

public class IsProjectMemberQueryHandler(TaskManagerDbContext dbContext) : IRequestHandler<IsProjectMemberQuery, bool>
{
    public Task<bool> Handle(IsProjectMemberQuery request, CancellationToken cancellationToken)
        => dbContext.ProjectMembers.AnyAsync(
            item => item.ProjectId == request.ProjectId && item.AccountLogin == request.UserLogin,
            cancellationToken);
}
