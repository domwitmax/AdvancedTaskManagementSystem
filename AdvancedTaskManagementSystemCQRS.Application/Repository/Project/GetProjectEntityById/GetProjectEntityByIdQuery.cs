using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Project.GetProjectEntityById;

public record GetProjectEntityByIdQuery(int ProjectId) : IRequest<ProjectEntityDTO?>;