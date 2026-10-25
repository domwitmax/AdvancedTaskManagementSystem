namespace AdvancedTaskManagementSystemCQRS.WebApi.Projects.Request
{
    public class UpdateProjectRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
