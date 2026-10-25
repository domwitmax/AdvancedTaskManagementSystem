namespace AdvancedTaskManagementSystemCRUD.Application.Services.Project.DTOs;

public class AddProjectMemberDTO
{
    public int ProjectId { get; set; }
    public string UserLogin { get; set; } = null!;
    public string AddedBy { get; set; } = null!;
}
