using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetPriorityEntities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.GetPriorityEntities
{
    public class GetPriorityEntitiesQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetPriorityEntitiesQuery, List<PriorityEntityDTO>>
    {
        public async Task<List<PriorityEntityDTO>> Handle(GetPriorityEntitiesQuery request, CancellationToken cancellationToken)
        {
            var priorities = dbContext.Priorities.ToList();
            return mapper.Map<List<PriorityEntityDTO>>(priorities);
        }
    }
}
