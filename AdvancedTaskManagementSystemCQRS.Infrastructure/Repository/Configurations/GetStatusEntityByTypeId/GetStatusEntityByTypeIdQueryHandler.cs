using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.GetStatusEntityByTypeId;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.GetStatusEntityByTypeId;

public class GetStatusEntityByTypeIdQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetStatusEntityByTypeIdQuery, List<StatusEntityDTO>>
{
    public async Task<List<StatusEntityDTO>> Handle(GetStatusEntityByTypeIdQuery request, CancellationToken cancellationToken)
    {
        var statuses = dbContext.Status.Where(s => s.TypeId == request.typeId).ToList();
        return mapper.Map<List<StatusEntityDTO>>(statuses);
    }
}
