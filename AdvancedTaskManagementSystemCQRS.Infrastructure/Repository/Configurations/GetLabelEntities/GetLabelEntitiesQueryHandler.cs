using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetLabelEntities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.GetLabelEntities
{
    public class GetLabelEntitiesQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetLabelEntitiesQuery, List<LabelEntityDTO>>
    {
        public async Task<List<LabelEntityDTO>> Handle(GetLabelEntitiesQuery request, CancellationToken cancellationToken)
        {
            var labels = await dbContext.Labels.ToListAsync(cancellationToken);
            return mapper.Map<List<LabelEntityDTO>>(labels);
        }
    }
}
