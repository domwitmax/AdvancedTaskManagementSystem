namespace AdvancedTaskManagementSystemCQRS.WebApi.Request
{
    public class UpdateStatusRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsFinished { get; set; }
    }
}
