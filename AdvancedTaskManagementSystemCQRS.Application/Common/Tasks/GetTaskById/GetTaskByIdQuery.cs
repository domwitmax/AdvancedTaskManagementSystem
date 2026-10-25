using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetTaskById
{
    public record GetTaskByIdQuery(int TaskId) : IRequest<GetTaskDTO?>;
}
