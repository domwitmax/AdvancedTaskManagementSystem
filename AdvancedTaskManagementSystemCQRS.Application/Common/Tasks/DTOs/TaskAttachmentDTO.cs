namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;

public class TaskAttachmentDTO
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public long FileSize { get; set; }
    public byte[]? Content { get; set; }
    public string UploadedBy { get; set; }
    public DateTime UploadedAt { get; set; }
}
