using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetProjectsByUser;

public record GetProjectsByUserQuery(string UserName) : IRequest<List<ProjectDTO>>;
