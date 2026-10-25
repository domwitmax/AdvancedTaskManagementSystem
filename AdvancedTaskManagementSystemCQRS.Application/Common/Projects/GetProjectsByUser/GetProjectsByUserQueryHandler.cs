using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.GetProjectEntitiesByUser;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetProjectsByUser;

public class GetProjectsByUserQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetProjectsByUserQuery, List<ProjectDTO>>
{
    public async Task<List<ProjectDTO>> Handle(GetProjectsByUserQuery request, CancellationToken cancellationToken)
    {
        var projects = await mediator.Send(new GetProjectEntitiesByUserQuery(request.UserName), cancellationToken);
        return mapper.Map<List<ProjectDTO>>(projects);
    }
}
