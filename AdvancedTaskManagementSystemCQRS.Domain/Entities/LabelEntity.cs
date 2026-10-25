namespace AdvancedTaskManagementSystemCQRS.Domain.Entities;

public class LabelEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<TaskLabelEntity> TaskLabels { get; set; } = new List<TaskLabelEntity>();
}
