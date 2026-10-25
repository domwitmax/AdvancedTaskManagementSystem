namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DTOs;

public class ProjectMemberEntityDTO
{
    public int ProjectId { get; set; }
    public string UserLogin { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int RoleId { get; set; }
    public string RoleName { get; set; } = null!;
    public DateTime AddedAt { get; set; }
    public string AddedBy { get; set; } = null!;
}
