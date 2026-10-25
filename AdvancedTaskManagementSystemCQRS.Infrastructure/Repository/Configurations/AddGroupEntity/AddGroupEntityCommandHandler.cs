using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddGroupEntity;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.AddGroupEntity;

public class AddGroupEntityCommandHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<AddGroupEntityCommand, int>
{
    public async Task<int> Handle(AddGroupEntityCommand request, CancellationToken cancellationToken)
    {
        var group = mapper.Map<GroupEntity>(request);
        dbContext.Groups.Add(group);
        await dbContext.SaveChangesAsync(cancellationToken);
        return group.Id;
    }
}
