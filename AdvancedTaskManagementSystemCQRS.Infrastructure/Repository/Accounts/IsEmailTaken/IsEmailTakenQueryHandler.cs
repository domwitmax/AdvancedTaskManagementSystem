using AdvancedTaskManagementSystemCQRS.Application.Repository.Accounts.IsEmailTaken;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Accounts.IsEmailTaken
{
    public class IsEmailTakenQueryHandler(TaskManagerDbContext dbContext) : IRequestHandler<IsEmailTakenQuery, bool>
    {
        public async Task<bool> Handle(IsEmailTakenQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.Account.Where(x => x.Email == request.Email).AnyAsync(cancellationToken);
        }
    }
}
