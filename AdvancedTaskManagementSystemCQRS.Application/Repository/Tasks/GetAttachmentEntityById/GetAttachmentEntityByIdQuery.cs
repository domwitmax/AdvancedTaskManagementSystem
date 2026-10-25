using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetAttachmentEntityById;

public record GetAttachmentEntityByIdQuery(int AttachmentId) : IRequest<TaskAttachmentEntityDTO?>;
