using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetPriorityEntities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetPriorities;

public class GetPrioritiesQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetPrioritiesQuery, List<PriorityDTO>>
{
    public async Task<List<PriorityDTO>> Handle(GetPrioritiesQuery request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetPriorityEntitiesQuery(), cancellationToken);
        return mapper.Map<List<PriorityDTO>>(result);
    }
}
