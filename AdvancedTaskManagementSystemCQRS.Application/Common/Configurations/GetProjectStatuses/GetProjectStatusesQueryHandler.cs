using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetProjectStatusEntities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetProjectStatuses;

public class GetProjectStatusesQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetProjectStatusesQuery, List<ProjectStatusDTO>>
{
    public async Task<List<ProjectStatusDTO>> Handle(GetProjectStatusesQuery request, CancellationToken cancellationToken)
    {
        var statuses = await mediator.Send(new GetProjectStatusEntitiesQuery(), cancellationToken);
        return mapper.Map<List<ProjectStatusDTO>>(statuses);
    }
}
