using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddTypeEntity;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.AddTypeEntity
{
    public class AddTypeEntityCommandHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<AddTypeEntityCommand, int>
    {
        public async Task<int> Handle(AddTypeEntityCommand request, CancellationToken cancellationToken)
        {
            var type = mapper.Map<TypeEntity>(request);
            dbContext.Type.Add(type);
            await dbContext.SaveChangesAsync(cancellationToken);
            return type.Id;
        }
    }
}
