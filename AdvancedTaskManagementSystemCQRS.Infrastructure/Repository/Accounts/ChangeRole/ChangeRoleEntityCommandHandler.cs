using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.ChangeRole;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Account.ChangeRole;

public class ChangeRoleEntityCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<ChangeRoleEntityCommand, bool>
{
    public async Task<bool> Handle(ChangeRoleEntityCommand request, CancellationToken cancellationToken)
    {
        var account = await dbContext.Account.FirstOrDefaultAsync(item => item.Login == request.Login, cancellationToken);
        if (account == null || !await dbContext.Roles.AnyAsync(item => item.Id == request.RoleId, cancellationToken))
            return false;

        if (account.RoleId == 1 && request.RoleId != 1)
        {
            var administratorsCount = await dbContext.Account.CountAsync(item => item.RoleId == 1, cancellationToken);
            if (administratorsCount <= 1)
                return false;
        }

        account.RoleId = request.RoleId;
        account.UpdatedAt = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
