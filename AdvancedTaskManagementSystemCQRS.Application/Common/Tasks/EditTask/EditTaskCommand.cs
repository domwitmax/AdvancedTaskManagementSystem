using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.EditTask
{
    public class EditTaskCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int ProjectId { get; set; }
        public int PriorityId { get; set; }
        public DateTime? DueDate { get; set; }
        public string AssignedTo { get; set; }
        public string ChangedBy { get; set; }
        public List<int> LabelIds { get; set; } = new();
    }
}
