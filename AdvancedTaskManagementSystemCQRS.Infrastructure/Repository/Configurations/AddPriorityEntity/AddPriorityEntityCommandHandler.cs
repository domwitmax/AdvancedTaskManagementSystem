using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddPriorityEntity;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.AddPriorityEntity
{
    public class AddPriorityEntityCommandHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<AddPriorityEntityCommand, int>
    {
        public async Task<int> Handle(AddPriorityEntityCommand request, CancellationToken cancellationToken)
        {
            var priority = mapper.Map<PriorityEntity>(request);
            dbContext.Priorities.Add(priority);
            await dbContext.SaveChangesAsync(cancellationToken);
            return priority.Id;
        }
    }
}
