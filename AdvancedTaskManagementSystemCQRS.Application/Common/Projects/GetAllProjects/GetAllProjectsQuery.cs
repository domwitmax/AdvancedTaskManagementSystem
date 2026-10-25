using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetAllProjects;

public record GetAllProjectsQuery() : IRequest<List<ProjectDTO>>;
