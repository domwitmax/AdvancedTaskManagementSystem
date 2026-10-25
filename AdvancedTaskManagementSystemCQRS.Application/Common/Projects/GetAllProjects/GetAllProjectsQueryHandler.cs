using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.GetAllProjectEntities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetAllProjects;

public class GetAllProjectsQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetAllProjectsQuery, List<ProjectDTO>>
{
    public async Task<List<ProjectDTO>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var command = new GetAllProjectEntitiesQuery();
        var projects = await mediator.Send(command, cancellationToken);
        return mapper.Map<List<ProjectDTO>>(projects);
    }
}
