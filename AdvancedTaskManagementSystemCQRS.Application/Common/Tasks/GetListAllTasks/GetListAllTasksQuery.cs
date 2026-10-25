using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetListAllTasks
{
    public class GetListAllTasksQuery : IRequest<List<GetTaskDTO>>
    {
    }
}
