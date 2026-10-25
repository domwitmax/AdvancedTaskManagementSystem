using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntities
{
    public class GetTaskEntitiesQuery : IRequest<List<TaskEntityDTO>>
    {
    }
}
