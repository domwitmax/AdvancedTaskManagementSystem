using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddStatusEntity;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.AddStatusEntity
{
    public class AddStatusEntityCommandHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<AddStatusEntityCommand, int>
    {
        public async Task<int> Handle(AddStatusEntityCommand request, CancellationToken cancellationToken)
        {
            var status = mapper.Map<StatusEntity>(request);
            dbContext.Status.Add(status);
            await dbContext.SaveChangesAsync(cancellationToken);
            return status.Id;
        }
    }
}
