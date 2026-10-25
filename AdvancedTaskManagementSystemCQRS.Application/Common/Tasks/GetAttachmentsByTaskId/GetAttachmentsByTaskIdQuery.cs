using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetAttachmentsByTaskId;

public record GetAttachmentsByTaskIdQuery(int TaskId) : IRequest<List<TaskAttachmentDTO>>;
