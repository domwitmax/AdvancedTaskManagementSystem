using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.ChangeTaskStatus
{
    public record ChangeTaskStatusCommand(int Id, int StatusId) : IRequest<int>
    {
        public string ChangedBy { get; set; }
    }
}
