namespace AdvancedTaskManagementSystemCRUD.WebApi.Request;

public class AddProjectMemberRequest
{
    public int ProjectId { get; set; }
    public string UserLogin { get; set; } = null!;
}
