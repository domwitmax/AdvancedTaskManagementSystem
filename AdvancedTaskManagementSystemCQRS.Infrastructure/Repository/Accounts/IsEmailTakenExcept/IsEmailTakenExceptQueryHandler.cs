using AdvancedTaskManagementSystemCQRS.Application.Repository.Accounts.IsEmailTakenExcept;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Accounts.IsEmailTakenExcept
{
    public class IsEmailTakenExceptQueryHandler(TaskManagerDbContext dbContext) : IRequestHandler<IsEmailTakenExceptQuery, bool>
    {
        public async Task<bool> Handle(IsEmailTakenExceptQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.Account.Where(x => x.Email == request.Email && x.Login != request.Login).AnyAsync(cancellationToken);
        }
    }
}
