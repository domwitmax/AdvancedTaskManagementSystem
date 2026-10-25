using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetStatusEntityByTypeId;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetStatusByTypeId;

public class GetStatusByTypeIdQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetStatusByTypeIdQuery, List<StatusDTO>>
{
    public async Task<List<StatusDTO>> Handle(GetStatusByTypeIdQuery request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetStatusEntityByTypeIdQuery(request.TypeId), cancellationToken);
        return mapper.Map<List<StatusDTO>>(result);
    }
}
