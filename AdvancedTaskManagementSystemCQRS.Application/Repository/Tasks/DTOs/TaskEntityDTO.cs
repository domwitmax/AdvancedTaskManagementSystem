namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs
{
    public class TaskEntityDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int PriorityId { get; set; }
        public string Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string AssignedTo { get; set; }
        public string CreatedBy { get; set; }
        public List<int> LabelIds { get; set; } = new();
        public List<string> Labels { get; set; } = new();
    }
}
