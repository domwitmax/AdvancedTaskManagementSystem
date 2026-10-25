namespace AdvancedTaskManagementSystemCRUD.WebApi.Request
{
    public class CreateTaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int ProjectId { get; set; }
        public int PriorityId { get; set; }
        public DateTime? DueDate { get; set; }
        public string AssignedTo { get; set; }
        public List<int> LabelIds { get; set; } = new();
    }
}
