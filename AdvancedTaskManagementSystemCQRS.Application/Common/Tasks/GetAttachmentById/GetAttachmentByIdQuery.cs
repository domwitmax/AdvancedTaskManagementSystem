using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetAttachmentById;

public record GetAttachmentByIdQuery(int AttachmentId) : IRequest<TaskAttachmentDTO?>;
