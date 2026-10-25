namespace AdvancedTaskManagementSystemCRUD.WebApi.Request
{
    public class ChangeTaskStatusRequest
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
    }
}
