namespace AdvancedTaskManagementSystemCQRS.Domain.Entities;

public class TaskEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int StatusId { get; set; }
    public int TypeId { get; set; }
    public int ProjectId { get; set; }
    public int PriorityId { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string AssignedTo { get; set; }
    public string CreatedBy { get; set; }
    public virtual AccountEntity Creator { get; set; }
    public virtual StatusEntity Status { get; set; }
    public virtual TypeEntity Type { get; set; }
    public virtual PriorityEntity Priority { get; set; }
    public virtual ProjectEntity Project { get; set; }
    public virtual ICollection<CommentEntity> Comments { get; set; }
    public virtual ICollection<TaskLabelEntity> TaskLabels { get; set; } = new List<TaskLabelEntity>();
    public virtual ICollection<TaskAttachmentEntity> Attachments { get; set; } = new List<TaskAttachmentEntity>();
}
