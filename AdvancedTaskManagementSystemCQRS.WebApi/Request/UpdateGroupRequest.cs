namespace AdvancedTaskManagementSystemCQRS.WebApi.Request;

public class UpdateGroupRequest
{
    public string OldName { get; set; }
    public string NewName { get; set; }
}