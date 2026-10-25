using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.GetProjectMemberEntities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetProjectMembers;

public class GetProjectMembersQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetProjectMembersQuery, List<ProjectMemberDTO>>
{
    public async Task<List<ProjectMemberDTO>> Handle(GetProjectMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await mediator.Send(new GetProjectMemberEntitiesQuery(request.ProjectId), cancellationToken);
        return mapper.Map<List<ProjectMemberDTO>>(members);
    }
}
