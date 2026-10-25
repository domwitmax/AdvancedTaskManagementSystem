using AdvancedTaskManagementSystemCQRS.Application.Repository.Accounts.IsLoginTaken;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Accounts.IsLoginTaken
{
    public class IsLoginTakenQueryHandler(TaskManagerDbContext dbContext) : IRequestHandler<IsLoginTakenQuery, bool>
    {
        public async Task<bool> Handle(IsLoginTakenQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.Account.Where(x => x.Login == request.Login).AnyAsync(cancellationToken);
        }
    }
}
