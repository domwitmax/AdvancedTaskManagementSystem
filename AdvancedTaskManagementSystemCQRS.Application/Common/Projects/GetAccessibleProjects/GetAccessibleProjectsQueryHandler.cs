using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetAllProjects;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetProjectsByUser;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetAccessibleProjects;

public class GetAccessibleProjectsQueryHandler(IMediator mediator)
    : IRequestHandler<GetAccessibleProjectsQuery, List<ProjectDTO>>
{
    public async Task<List<ProjectDTO>> Handle(
        GetAccessibleProjectsQuery request,
        CancellationToken cancellationToken)
    {
        if (request.IsAdministrator)
            return await mediator.Send(new GetAllProjectsQuery(), cancellationToken);

        if (string.IsNullOrWhiteSpace(request.UserLogin))
            return new List<ProjectDTO>();

        return await mediator.Send(
            new GetProjectsByUserQuery(request.UserLogin),
            cancellationToken);
    }
}
