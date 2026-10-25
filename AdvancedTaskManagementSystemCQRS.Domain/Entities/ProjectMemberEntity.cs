namespace AdvancedTaskManagementSystemCQRS.Domain.Entities;

public class ProjectMemberEntity
{
    public int ProjectId { get; set; }
    public string AccountLogin { get; set; } = null!;
    public DateTime AddedAt { get; set; }
    public string AddedBy { get; set; } = null!;
    public virtual ProjectEntity Project { get; set; } = null!;
    public virtual AccountEntity Account { get; set; } = null!;
}
