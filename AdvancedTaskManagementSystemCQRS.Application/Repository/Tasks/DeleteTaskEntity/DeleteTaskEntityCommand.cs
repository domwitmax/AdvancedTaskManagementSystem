using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DeleteTaskEntity
{
    public record DeleteTaskEntityCommand(int Id) : IRequest<bool>;
}
