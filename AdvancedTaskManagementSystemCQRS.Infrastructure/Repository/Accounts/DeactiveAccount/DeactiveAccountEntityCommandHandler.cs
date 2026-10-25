using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.DeactiveAccount;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Account.DeactiveAccount;

public class DeactiveAccountEntityCommandHandler(TaskManagerDbContext dbContext) : IRequestHandler<DeactiveAccountEntityCommand, bool>
{
    public async Task<bool> Handle(DeactiveAccountEntityCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Account.FirstOrDefaultAsync(item => item.Login == request.UserName, cancellationToken);
        if (user == null)
            return false;

        user.Active = false;
        user.UpdatedAt = DateTime.UtcNow;
        dbContext.Account.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
