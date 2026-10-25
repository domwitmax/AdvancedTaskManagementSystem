using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetListTasksByCreator
{
    public record GetListTasksByCreatorQuery(string Creator) : IRequest<List<GetTaskDTO>>;
}
