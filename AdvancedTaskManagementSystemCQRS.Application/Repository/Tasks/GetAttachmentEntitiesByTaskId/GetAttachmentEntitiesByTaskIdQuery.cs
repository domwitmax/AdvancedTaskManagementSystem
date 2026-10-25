using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetAttachmentEntitiesByTaskId;

public record GetAttachmentEntitiesByTaskIdQuery(int TaskId) : IRequest<List<TaskAttachmentEntityDTO>>;
