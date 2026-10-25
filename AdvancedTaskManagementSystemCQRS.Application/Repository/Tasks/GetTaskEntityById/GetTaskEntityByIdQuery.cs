using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntityById
{
    public record GetTaskEntityByIdQuery(int Id) : IRequest<TaskEntityDTO?>;
}
