namespace AdvancedTaskManagementSystemCRUD.WebApi.Request
{
    public class UpdateProjectRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int StatusId { get; set; }
    }
}
