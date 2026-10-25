using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.AddTaskAttachmentEntity;

public record AddTaskAttachmentEntityCommand(
    int TaskId,
    string FileName,
    string ContentType,
    long FileSize,
    byte[] Content,
    string UploadedBy) : IRequest<int?>
{
    public List<TaskChangeDetailDTO> ChangeDetails { get; set; } = new();
}
