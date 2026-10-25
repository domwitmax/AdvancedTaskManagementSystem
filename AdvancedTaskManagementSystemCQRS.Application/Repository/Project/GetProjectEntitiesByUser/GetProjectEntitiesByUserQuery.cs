using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Project.GetProjectEntitiesByUser;

public record GetProjectEntitiesByUserQuery(string UserName) : IRequest<List<ProjectEntityDTO>>;
