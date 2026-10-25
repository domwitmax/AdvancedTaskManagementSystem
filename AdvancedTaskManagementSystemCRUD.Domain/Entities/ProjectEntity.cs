namespace AdvancedTaskManagementSystemCRUD.Domain.Entities;

public class ProjectEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int StatusId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public virtual ProjectStatusEntity Status { get; set; } = null!;
    public virtual ICollection<ProjectMemberEntity> ProjectMembers { get; set; } = new List<ProjectMemberEntity>();
}
