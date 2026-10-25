namespace AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;

public class CommentDTO
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public int TaskId { get; set; }
}
