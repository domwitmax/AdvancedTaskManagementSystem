namespace AdvancedTaskManagementSystemCQRS.Domain.Entities;

public class CommentEntity
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public int TaskId { get; set; }
    public TaskEntity Task { get; set; }
}