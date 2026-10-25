using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddProjectStatusEntity;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.AddProjectStatusEntity;

public class AddProjectStatusEntityCommandHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<AddProjectStatusEntityCommand, int>
{
    public async Task<int> Handle(AddProjectStatusEntityCommand request, CancellationToken cancellationToken)
    {
        var status = mapper.Map<ProjectStatusEntity>(request);
        dbContext.ProjectStatuses.Add(status);
        await dbContext.SaveChangesAsync(cancellationToken);
        return status.Id;
    }
}
