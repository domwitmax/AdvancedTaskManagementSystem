using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DeleteProjectEntity;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DeleteProject;

public class DeleteProjectCommandHandler(IMediator mediator) : IRequestHandler<DeleteProjectCommand, int>
{
    public async Task<int> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        return await mediator.Send(new DeleteProjectEntityCommand(request.ProjectId), cancellationToken);
    }
}
