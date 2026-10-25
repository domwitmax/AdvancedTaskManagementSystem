using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.UpdateLastLogin;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Account.UpdateLastLogin;

public class UpdateLastLoginEntityCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<UpdateLastLoginEntityCommand, bool>
{
    public async Task<bool> Handle(UpdateLastLoginEntityCommand request, CancellationToken cancellationToken)
    {
        var account = await dbContext.Account.FirstOrDefaultAsync(item => item.Login == request.Login, cancellationToken);
        if (account == null)
            return false;

        account.LastLoginAt = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
