namespace AdvancedTaskManagementSystemCQRS.WebApi.Projects.Request;

public class AddProjectMemberRequest
{
    public int ProjectId { get; set; }
    public string UserLogin { get; set; } = null!;
}
