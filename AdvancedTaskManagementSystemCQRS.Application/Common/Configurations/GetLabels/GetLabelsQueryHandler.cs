using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetLabelEntities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetLabels;

public class GetLabelsQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetLabelsQuery, List<LabelDTO>>
{
    public async Task<List<LabelDTO>> Handle(GetLabelsQuery request, CancellationToken cancellationToken)
    {
        var labels = await mediator.Send(new GetLabelEntitiesQuery(), cancellationToken);
        return mapper.Map<List<LabelDTO>>(labels);
    }
}
