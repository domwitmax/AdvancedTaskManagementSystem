using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.AddTaskAttachment;

public record AddTaskAttachmentCommand(
    int TaskId,
    string FileName,
    string ContentType,
    long FileSize,
    byte[] Content,
    string UploadedBy) : IRequest<int?>;
