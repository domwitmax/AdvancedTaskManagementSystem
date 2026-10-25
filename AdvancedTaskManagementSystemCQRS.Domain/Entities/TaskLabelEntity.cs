namespace AdvancedTaskManagementSystemCQRS.Domain.Entities;

public class TaskLabelEntity
{
    public int TaskId { get; set; }
    public int LabelId { get; set; }
    public virtual TaskEntity Task { get; set; }
    public virtual LabelEntity Label { get; set; }
}
