using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.IsProjectMember;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CanAccessProject;

public class CanAccessProjectQueryHandler(IMediator mediator) : IRequestHandler<CanAccessProjectQuery, bool>
{
    public async Task<bool> Handle(CanAccessProjectQuery request, CancellationToken cancellationToken)
    {
        if (request.IsAdministrator)
            return true;

        if (string.IsNullOrWhiteSpace(request.UserLogin))
            return false;

        return await mediator.Send(
            new IsProjectMemberQuery(request.ProjectId, request.UserLogin),
            cancellationToken);
    }
}
