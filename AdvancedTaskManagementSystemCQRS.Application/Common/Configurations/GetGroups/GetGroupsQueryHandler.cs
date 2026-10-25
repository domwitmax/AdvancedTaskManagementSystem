using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetGroupEntities;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetGroups;

public class GetGroupsQueryHandler(IMediator mediator) : IRequestHandler<GetGroupsQuery, List<string>>
{
    public async Task<List<string>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetGroupEntitiesQuery(), cancellationToken);
    }
}
