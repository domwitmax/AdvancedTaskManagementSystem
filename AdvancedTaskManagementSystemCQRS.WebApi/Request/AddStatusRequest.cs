namespace AdvancedTaskManagementSystemCQRS.WebApi.Request;

public class AddStatusRequest
{
    public string Name { get; set; }
    public int TypeId { get; set; }
    public bool IsFinished { get; set; }
}