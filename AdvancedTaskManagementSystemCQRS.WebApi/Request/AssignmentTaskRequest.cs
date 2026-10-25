namespace AdvancedTaskManagementSystemCQRS.WebApi.Request
{
    public class AssignmentTaskRequest
    {
        public int Id { get; set; }
        public string AssignedTo { get; set; }
    }
}
