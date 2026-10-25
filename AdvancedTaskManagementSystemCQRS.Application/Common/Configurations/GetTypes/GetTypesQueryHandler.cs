using AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetTypeEntities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetTypes;

public class GetTypesQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetTypesQuery, List<TypeDTO>>
{
    public async Task<List<TypeDTO>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetTypeEntitiesQuery(), cancellationToken);
        return mapper.Map<List<TypeDTO>>(result);
    }
}
