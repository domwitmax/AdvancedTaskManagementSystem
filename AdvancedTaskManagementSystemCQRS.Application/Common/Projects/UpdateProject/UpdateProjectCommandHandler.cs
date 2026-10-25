using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.UpdateProjectEntity;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.UpdateProject;

public class UpdateProjectCommandHandler(IMediator mediator, IMapper mapper) : IRequestHandler<UpdateProjectCommand, int?>
{
    public async Task<int?> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<UpdateProjectEntityCommand>(request);
        return await mediator.Send(command);
    }
}
