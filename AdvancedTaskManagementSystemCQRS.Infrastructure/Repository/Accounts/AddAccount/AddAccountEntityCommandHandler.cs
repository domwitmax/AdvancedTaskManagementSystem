using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.AddAccount;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Account.AddAccount;

public class AddAccountEntityCommandHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<AddAccountEntityCommand>
{
    public async Task Handle(AddAccountEntityCommand request, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var user = mapper.Map<AccountEntity>(request);
        user.CreatedAt = now;
        user.UpdatedAt = now;
        user.LastLoginAt = now;
        user.LastGetUserAt = now;
        user.Active = true;
        var isFirstAccount = !await dbContext.Account.AnyAsync(cancellationToken);
        user.RoleId = isFirstAccount ? 1 : 3;

        dbContext.Account.Add(user);

        if (isFirstAccount)
        {
            var projectIds = await dbContext.Projects
                .Where(project => !project.ProjectMembers.Any())
                .Select(project => project.Id)
                .ToListAsync(cancellationToken);

            dbContext.ProjectMembers.AddRange(projectIds.Select(projectId => new ProjectMemberEntity
            {
                ProjectId = projectId,
                AccountLogin = user.Login,
                AddedAt = now,
                AddedBy = user.Login
            }));
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
