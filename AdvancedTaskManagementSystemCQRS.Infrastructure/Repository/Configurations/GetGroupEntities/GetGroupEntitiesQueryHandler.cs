using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetGroupEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.GetGroupEntities
{
    public class GetGroupEntitiesQueryHandler(TaskManagerDbContext dbContext) : IRequestHandler<GetGroupEntitiesQuery, List<string>>
    {
        public async Task<List<string>> Handle(GetGroupEntitiesQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.Groups
                .AsNoTracking()
                .OrderBy(item => item.Name)
                .Select(item => item.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
