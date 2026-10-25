using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.AssignmentTask
{
    public class AssignmentTaskCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string AssignedTo { get; set; }
        public string ChangedBy { get; set; }
    }
}
