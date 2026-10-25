using AdvancedTaskManagementSystemCQRS.Application.Common.Reports.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetProjectEntitiesProgress;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Reports.GetProjectProgress;

public class GetProjectProgressQueryHandler(IMediator mediator)
    : IRequestHandler<GetProjectProgressQuery, ProjectProgressDTO?>
{
    public async Task<ProjectProgressDTO?> Handle(GetProjectProgressQuery request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetProjectEntitiesProgressQuery(request.ProjectId), cancellationToken);
    }
}
