using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.GetProjectEntityById;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetProjectById;

public class GetProjectByIdQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetProjectByIdQuery, ProjectDTO?>
{
    public async Task<ProjectDTO?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await mediator.Send(new GetProjectEntityByIdQuery(request.ProjectId), cancellationToken);

        return mapper.Map<ProjectDTO>(project);
    }
}
