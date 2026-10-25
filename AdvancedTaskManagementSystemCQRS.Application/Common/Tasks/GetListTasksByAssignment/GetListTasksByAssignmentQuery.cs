using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetListTasksByAssignment
{
    public record GetListTasksByAssignmentQuery(string Assignment) : IRequest<List<GetTaskDTO>>;
}
