using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.CreateProjectEntity;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CreateProject;

public class CreateProjectCommandHandler(IMediator mediator, IMapper mapper) : IRequestHandler<CreateProjectCommand, int>
{
    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var command = mapper.Map<CreateProjectEntityCommand>(request);
        return await mediator.Send(command, cancellationToken);
    }
}
