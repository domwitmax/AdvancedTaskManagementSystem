using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetTypeEntities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.GetTypeEntities
{
    public class GetTypeEntitiesQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetTypeEntitiesQuery, List<TypeEntityDTO>>
    {
        public async Task<List<TypeEntityDTO>> Handle(GetTypeEntitiesQuery request, CancellationToken cancellationToken)
        {
            var types = dbContext.Type.ToList();
            return mapper.Map<List<TypeEntityDTO>>(types);
        }
    }
}
