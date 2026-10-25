using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DeleteTaskById
{
    public record DeleteTaskByIdCommand(int Id) : IRequest<bool>;
}
